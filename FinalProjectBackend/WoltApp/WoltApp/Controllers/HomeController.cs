using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoltBusiness.DTOs;
using WoltDataAccess.DAL;

namespace WoltApp.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context { get; }
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> IndexAsync()
        {
            HomeDTO homeDTO = new HomeDTO
            {
                Sliders = await _context.Sliders.ToListAsync(),
                Categories = await _context.Categories
                .Where(c => c.IsDeleted == false && c.ImageURL!=null)
                .ToListAsync(),
                Restaurants = await _context.Restaurants.Where(r => r.IsDeleted == false).Include(r=>r.RestaurantProducts).ThenInclude(r=>r.Product).ToListAsync(),
                Stores = await _context.Stores.Where(s => s.IsDeleted == false).ToListAsync()
            };
            return View(homeDTO);
        }
    }
}
