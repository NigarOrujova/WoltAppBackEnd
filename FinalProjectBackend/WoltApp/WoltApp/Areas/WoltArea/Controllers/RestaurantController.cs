﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoltBusiness.DTOs;
using WoltDataAccess.DAL;
using WoltEntity.Entities;
using WoltEntity.Utilities.File;

namespace WoltApp.Areas.WoltArea.Controllers
{
    [Area("WoltArea")]
    [Authorize(Roles = "Admin")]
    public class RestaurantController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;

        public RestaurantController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async  Task<IActionResult> Index(int page = 1, int take = 5)
        {
            List<Restaurant> restaurants = await _context.Restaurants
                                                         .OrderByDescending(p => p.Id)
                                                         .Skip((page - 1) * take)
                                                         .Take(take)
                                                         .Include(x=>x.RestaurantCategories).ThenInclude(x=>x.Category)
                                                         .Include(x=>x.RestaurantProducts).ThenInclude(x=>x.Product)
                                                         .ToListAsync();
            var restaurantVms = GetRestaurantList(restaurants);
            int pageCount = GetPageCount(take);
            Paginate<RestaurantDTO> model = new Paginate<RestaurantDTO>(restaurantVms, page, pageCount);
            return View(model);
        }
        private int GetPageCount(int take)
        {
            var restaurantCount = _context.Restaurants.Count();
            return (int)Math.Ceiling(((decimal)restaurantCount / take)+1);
        }
        private List<RestaurantDTO> GetRestaurantList(List<Restaurant> restaurants)
        {
            List<RestaurantDTO> model = new List<RestaurantDTO>();
            foreach (var item in restaurants)
            {
                var restaurant = new RestaurantDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Address=item.Address,
                    ContactNumber=item.ContactNumber,
                    DiscountPercent = item.DiscountPercent,
                    IsDeleted = item.IsDeleted,
                    IsNew = item.IsNew,
                    ImageURL = item.ImageURL,
                    HeroImageURL=item.HeroImageURL
                };
                model.Add(restaurant);
            }
            return model;
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
            if (!ModelState.IsValid) return RedirectToAction("Index","Error");
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
                IsDeleted=restaurant.IsDeleted,
                ImageURL =await restaurant.Photo.SaveFileAsync(_env.WebRootPath,"assets/img"),
                HeroImageURL = await restaurant.HeroPhoto.SaveFileAsync(_env.WebRootPath, "assets/img"),
                RestaurantCategories = restaurant.RestaurantCategories,
                RestaurantProducts = restaurant.RestaurantProducts,
                IsNew=true
            };
            await _context.Restaurants.AddAsync(newRestaurant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //GET - Update
        public async Task<IActionResult> Update(int? id)
        {
            Restaurant restaurant = await _context.Restaurants.Include(x => x.RestaurantProducts).ThenInclude(x => x.Product)
                                                              .Include(x => x.RestaurantCategories).ThenInclude(x => x.Category).Where(r => r.IsDeleted == false && r.Id == id).FirstOrDefaultAsync();
            if (restaurant.Id != id) return RedirectToAction("Index", "Error");
            ViewBag.Products = _context.Products.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            restaurant.CategoryIds = restaurant.RestaurantCategories.Select(x => x.CategoryId).ToList();
            restaurant.ProductIds = restaurant.RestaurantProducts.Select(x => x.ProductId).ToList();
            return View(restaurant);
        }

        //POST - Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Restaurant restaurant)
        {
            Restaurant resDb = await _context.Restaurants.Include(x => x.RestaurantProducts)
                                                         .ThenInclude(x => x.Product)
                                                         .Include(x => x.RestaurantCategories)
                                                         .ThenInclude(x => x.Category)
                                                         .FirstOrDefaultAsync(x => x.Id == restaurant.Id);
            if (resDb == null) return RedirectToAction("Index", "Error");
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
            setData(resDb, restaurant);
            resDb.RestaurantCategories.RemoveAll(x => !restaurant.CategoryIds.Contains(x.CategoryId));
            resDb.RestaurantProducts.RemoveAll(x => !restaurant.ProductIds.Contains(x.ProductId));
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
        private void setData(Restaurant resDb,Restaurant restaurant)
        {
            resDb.Name = restaurant.Name;
            resDb.Description = restaurant.Description;
            resDb.ContactNumber = restaurant.ContactNumber;
            resDb.Address = restaurant.Address;
            resDb.Discount = restaurant.Discount;
            resDb.IsNew = restaurant.IsNew;
            resDb.IsDeleted = restaurant.IsDeleted; ;
            resDb.DiscountPercent = restaurant.DiscountPercent;
        }

        //GET - Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Error");
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null) RedirectToAction("Index", "Error");

            return View(restaurant);
        }

        //POST - Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null) return RedirectToAction("Index", "Error");
            //restaurant.IsDeleted = true;
            Helper.RemoveFile(_env.WebRootPath, "assets/img", restaurant.ImageURL);
            Helper.RemoveFile(_env.WebRootPath, "assets/img", restaurant.HeroImageURL);
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Detail
        public async Task<IActionResult> Detail(int? Id)
        {
            if (Id == null) return RedirectToAction("Index", "Error");
            ViewBag.RestaurantCategories = await _context.RestaurantCategories.Include(x => x.Restaurant).Include(x=>x.Category).Where(x=>x.RestaurantId==Id).ToListAsync();
            ViewBag.RestaurantProducts = await _context.RestaurantProducts.Include(x=>x.Product).Where(x=>x.RestaurantId==Id).ToListAsync();

            return View(await _context.Restaurants.Include(x => x.RestaurantProducts).ThenInclude(x => x.Product).Include(x => x.RestaurantCategories).ThenInclude(x => x.Category).FirstOrDefaultAsync(c => c.Id == Id));
        }
    }
}
