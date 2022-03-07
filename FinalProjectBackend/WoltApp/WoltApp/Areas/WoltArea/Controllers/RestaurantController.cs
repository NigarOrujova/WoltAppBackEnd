using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoltDataAccess.DAL;
using WoltEntity.Entities;
using WoltEntity.Utilities.File;

namespace WoltApp.Areas.WoltArea.Controllers
{
    [Area("WoltArea")]
    public class RestaurantController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;

        public RestaurantController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async  Task<IActionResult> Index()
        {
            List<Restaurant> restaurants = await _context.Restaurants.Where(x => x.IsDeleted == false)
                                                         .Include(x=>x.RestaurantCategories).ThenInclude(x=>x.Category)
                                                         .Include(x=>x.RestaurantProducts).ThenInclude(x=>x.Product)
                                                         .ToListAsync();
            return View(restaurants);
        }
        //GET - Create
        public IActionResult Create()
        {
            ViewBag.Products = _context.Products.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }
        //POST - Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Create(Restaurant restaurant)
        {
            ViewBag.Products = _context.Products.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            if (!ModelState.IsValid) return View(restaurant);
            restaurant.RestaurantCategories =new List<RestaurantCategory>();
            restaurant.RestaurantProducts = new List<RestaurantProduct>();
            foreach (var id in restaurant.CategoryIds)
            {
                RestaurantCategory resCategory = new RestaurantCategory()
                {
                    CategoryId = id,
                    RestaurantId = restaurant.Id

                };
                restaurant.RestaurantCategories.Add(resCategory);
            }
            foreach (var id in restaurant.ProductIds)
            {
                RestaurantProduct resProduct = new RestaurantProduct()
                {
                    ProductId = id,
                    RestaurantId = restaurant.Id

                };
                restaurant.RestaurantProducts.Add(resProduct);
            }
            if (!restaurant.HeroPhoto.CheckFileSize(1000))
            {
                ModelState.AddModelError("HeroPhoto", "Size wrong");
                return View(restaurant);
            }
            if (!restaurant.HeroPhoto.CheckFileType("image/"))
            {
                ModelState.AddModelError("HeroPhoto", "Type Wrong");
                return View(restaurant);
            }
            if (!restaurant.Photo.CheckFileSize(1000))
            {
                ModelState.AddModelError("Photo", "Size wrong");
                return View(restaurant);
            }
            if (!restaurant.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Type Wrong");
                return View(restaurant);
            }
            Restaurant newRestaurant = new Restaurant()
            {
                Name = restaurant.Name,
                Description = restaurant.Description,
                Address=restaurant.Address,
                ContactNumber=restaurant.ContactNumber,
                ControllerName=restaurant.ControllerName,
                ImageURL =await restaurant.Photo.SaveFileAsync(_env.WebRootPath,"assets/img"),
                HeroImageURL = await restaurant.HeroPhoto.SaveFileAsync(_env.WebRootPath, "assets/img")
            };

            await _context.Restaurants.AddAsync(newRestaurant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
