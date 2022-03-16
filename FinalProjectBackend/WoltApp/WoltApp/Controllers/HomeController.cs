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
                RestaurantCategories = await _context.RestaurantCategories
                                       .Where(x=>x.Category.ImageURL!=null && x.Category.IsDeleted==false && x.Restaurant.Id==x.RestaurantId)
                                      .Include(x => x.Category)
                                      .Include(x => x.Restaurant).ToListAsync(),

                Categories = await _context.Categories
                .Where(c => c.IsDeleted == false && c.ImageURL != null)
                .ToListAsync(),
                StoreCategories =await _context.StoreCategories
                                              .Include(x=>x.Category)
                                              .Include(x=>x.Store).ToListAsync(),
                Restaurants = await _context.Restaurants
                                            .Include(r=>r.RestaurantProducts).ThenInclude(r=>r.Product)
                                            .ToListAsync(),
                Stores = await _context.Stores.ToListAsync()
            };
            return View(homeDTO);
        }
    }
}
