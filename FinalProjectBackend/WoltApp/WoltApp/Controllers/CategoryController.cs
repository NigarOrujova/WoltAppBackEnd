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
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? Id)
        {
            List<RestaurantCategory> RestaurantCategories = await _context.RestaurantCategories.Include(c => c.Restaurant)
                                                                          .Where(c => c.CategoryId == Id)
                                                                          .Include(c => c.Category).ToListAsync();
            return View(RestaurantCategories);
        }
    }
}
