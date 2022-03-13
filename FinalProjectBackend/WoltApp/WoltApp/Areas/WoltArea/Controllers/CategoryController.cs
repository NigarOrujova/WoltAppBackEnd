using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoltDataAccess.DAL;
using WoltEntity.Entities;

namespace WoltApp.Areas.WoltArea.Controllers
{
    [Area("WoltArea")]
    public class CategoryController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;

        public CategoryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _context.Categories.Where(x => x.IsDeleted == false)
                                             .Include(x => x.RestaurantCategories).ThenInclude(x => x.Category)
                                             .Include(x => x.StoreCategories).ThenInclude(x => x.Store)
                                             .ToListAsync();
            return View(categories);
        }
        //GET - Create
        public IActionResult Create()
        {
            ViewBag.restaurants = _context.Restaurants.ToList();
            ViewBag.stores = _context.Stores.ToList();
            return View();
        }
    }
}
