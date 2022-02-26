using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoltBusiness.DTOs;
using WoltDataAccess.DAL;
using WoltEntity.Entities;

namespace WoltApp.Controllers
{
    public class FrydayController : Controller
    {
        private AppDbContext _context { get; }

        public FrydayController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int id)
        {
            RestaurantDTO resDTO = new RestaurantDTO
            {
                RestaurantProducts = await _context.RestaurantProducts.Include(p => p.Restaurant)
                                                                      .Where(p => p.RestaurantId == 2)
                                                                      .Include(p => p.Product).ToListAsync(),
                RestaurantCategories = await _context.RestaurantCategories.Include(c => c.Restaurant)
                                                                          .Where(c => c.RestaurantId == 2)
                                                                          .Include(c => c.Category).ToListAsync(),
                Restaurant = await _context.Restaurants.Where(r => r.IsDeleted == false)
                                                       .Include(r=>r.RestaurantCategories).ThenInclude(r=>r.Category).FirstOrDefaultAsync(r=>r.Id==6)
            };
            return View(resDTO);
        }
    }
}
