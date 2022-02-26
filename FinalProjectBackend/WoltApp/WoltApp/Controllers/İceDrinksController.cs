using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoltDataAccess.DAL;
using WoltEntity.Entities;

namespace WoltApp.Controllers
{
    public class İceDrinksController : Controller
    {
        private AppDbContext _context { get; }

        public İceDrinksController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<RestaurantCategory> RestaurantCategories = await _context.RestaurantCategories.Include(c => c.Restaurant)
                                                                          .Where(c => c.CategoryId == 4)
                                                                          .Include(c => c.Category).ToListAsync();
            return View(RestaurantCategories);
        }
    }
}
