using Microsoft.AspNetCore.Identity;
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
    public class RestaurantController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        public RestaurantController(UserManager<AppUser> userManager
                                   ,AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index(int Id)
        {
            RestaurantDTO resDTO = new RestaurantDTO
            {
                RestaurantProducts = await _context.RestaurantProducts.Include(p => p.Product).Include(p => p.Restaurant)
                                                          .Where(p => p.RestaurantId == Id)
                                                          .ToListAsync(),
                RestaurantCategories = await _context.RestaurantCategories.Include(c => c.Category).Include(c => c.Restaurant)
                                                              .Where(c => c.RestaurantId == Id)
                                                              .ToListAsync(),
                Restaurant = await _context.Restaurants.Where(r => r.IsDeleted == false && r.Id==Id).FirstOrDefaultAsync(),
                
            };
            return View(resDTO);
        }
    }
}
