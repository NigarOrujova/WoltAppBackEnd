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
            if (restaurant.CategoryIds != null)
            {
                restaurant.RestaurantCategories = new List<RestaurantCategory>();
                foreach (var id in restaurant.CategoryIds)
                {
                    RestaurantCategory resCategory = new RestaurantCategory()
                    {
                        CategoryId = id,
                        Restaurant = restaurant

                    };
                    restaurant.RestaurantCategories.Add(resCategory);
                }
            }
            if (restaurant.ProductIds != null)
            {
                restaurant.RestaurantProducts = new List<RestaurantProduct>();
                foreach (var id in restaurant.ProductIds)
                {
                    RestaurantProduct resProduct = new RestaurantProduct()
                    {
                        ProductId = id,
                        Restaurant = restaurant

                    };
                    restaurant.RestaurantProducts.Add(resProduct);
                }
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
                ImageURL =await restaurant.Photo.SaveFileAsync(_env.WebRootPath,"assets/img"),
                HeroImageURL = await restaurant.HeroPhoto.SaveFileAsync(_env.WebRootPath, "assets/img"),
                RestaurantCategories = restaurant.RestaurantCategories,
                RestaurantProducts = restaurant.RestaurantProducts
            };

            await _context.Restaurants.AddAsync(newRestaurant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Products = _context.Products.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            Restaurant restaurant = await _context.Restaurants.Include(x => x.RestaurantProducts).Include(x => x.RestaurantCategories).Where(r => r.IsDeleted == false && r.Id == id).FirstOrDefaultAsync();
            if (restaurant.Id != id) return BadRequest();
            restaurant.CategoryIds =await _context.RestaurantCategories.Select(x => x.CategoryId).ToListAsync();
            restaurant.ProductIds = await _context.RestaurantProducts.Select(x => x.ProductId).ToListAsync();
            return View(restaurant);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Restaurant restaurant)
        {
            if (restaurant.Id != id) return BadRequest();
            Restaurant resDb = await _context.Restaurants.Include(x=>x.RestaurantProducts).Include(x=>x.RestaurantCategories).Where(cd => cd.IsDeleted == false && cd.Id == id).FirstOrDefaultAsync();
            if (resDb == null) return NotFound();
            if (restaurant.Description == null) return View(resDb);
            bool isExsistFile = true;
            if (restaurant.Photo == null && restaurant.HeroPhoto==null)
            {
                restaurant.ImageURL = resDb.ImageURL;
                restaurant.HeroImageURL = resDb.HeroImageURL;
                isExsistFile = false;
            }
            if (isExsistFile)
            {
                if (!restaurant.Photo.CheckFileSize(1000))
                {
                    ModelState.AddModelError("Photo", "Size wrong");
                    return View(resDb);
                }
                if (!restaurant.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "Type Wrong");
                    return View(resDb);

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
                Helper.RemoveFile(_env.WebRootPath, "assets/img", resDb.ImageURL);
                string newPhotoName = await restaurant.Photo.SaveFileAsync(_env.WebRootPath, "assets/img");
                resDb.ImageURL = newPhotoName;
                Helper.RemoveFile(_env.WebRootPath, "assets/img", resDb.HeroImageURL);
                string newHeroPhotoName = await restaurant.HeroPhoto.SaveFileAsync(_env.WebRootPath, "assets/img");
                resDb.HeroImageURL = newHeroPhotoName;
            }
            resDb.Name = restaurant.Name;
            resDb.Description = restaurant.Description;
            resDb.ContactNumber = restaurant.ContactNumber;
            resDb.Address = restaurant.Address;
            resDb.Discount = restaurant.Discount;
            resDb.IsNew = restaurant.IsNew;
            resDb.DiscountPercent = restaurant.DiscountPercent;
            foreach (var categoryId in restaurant.CategoryIds.Where(x => !resDb.RestaurantCategories.Any(rc => rc.CategoryId == x)))
            {
                RestaurantCategory restaurantCategory = new RestaurantCategory
                {
                    RestaurantId = restaurant.Id,
                    CategoryId = categoryId
                };

                resDb.RestaurantCategories.Add(restaurantCategory);
            }
            foreach (var productId in restaurant.ProductIds.Where(x => !resDb.RestaurantProducts.Any(rc => rc.ProductId == x)))
            {
                RestaurantProduct restaurantProduct = new RestaurantProduct
                {
                    RestaurantId = restaurant.Id,
                    ProductId = productId
                };

                resDb.RestaurantProducts.Add(restaurantProduct);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //GET - Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null) return BadRequest();

            return View(restaurant);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null) return NotFound();
            restaurant.IsDeleted = true;
            //Helper.RemoveFile(_env.WebRootPath, "assets/img", restaurant.ImageURL);
            //_context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
