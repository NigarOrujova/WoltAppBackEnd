using Microsoft.AspNetCore.Authorization;
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
    public class CategoryController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;

        public CategoryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 5)
        {
            List<Category> categories = await _context.Categories.Where(x => x.IsDeleted == false)
                                                                 .OrderByDescending(p => p.Id)
                                                                 .Skip((page - 1) * take)
                                                                 .Take(take)
                                                                 .Include(x => x.RestaurantCategories).ThenInclude(x => x.Category)
                                                                 .Include(x => x.StoreCategories).ThenInclude(x => x.Store)
                                                                 .ToListAsync();
            var categoryVms = GetCategoryList(categories);
            int pageCount = GetPageCount(take);
            Paginate<CategoryDTO> model = new Paginate<CategoryDTO>(categoryVms, page, pageCount);
            return View(model);
        }

        private List<CategoryDTO> GetCategoryList(List<Category> categories)
        {
            List<CategoryDTO> model = new List<CategoryDTO>();
            foreach (var item in categories)
            {
                var category = new CategoryDTO
                {
                    Id = item.Id,
                    Name=item.Name,
                    IsDeleted = item.IsDeleted,
                    ImageURL = item.ImageURL
                };
                model.Add(category);
            }
            return model;
        }

        private int GetPageCount(int take)
        {
            var productCount = _context.Categories.Where(p => p.IsDeleted == false).Count();
            return (int)Math.Ceiling(((decimal)productCount / take)+1);
        }

        //GET - Create
        public IActionResult Create()
        {
            ViewBag.restaurants = _context.Restaurants.ToList();
            ViewBag.stores = _context.Stores.ToList();
            return View();
        }

        //POST - Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            ViewBag.restaurants = _context.Restaurants.ToList();
            ViewBag.stores = _context.Stores.ToList();
            if (!ModelState.IsValid) return View(category);
            if (category.RestaurantIds != null)
            {
                category.RestaurantCategories = new List<RestaurantCategory>();
                foreach (var id in category.RestaurantIds)
                {
                    RestaurantCategory resCategory = new RestaurantCategory()
                    {
                        RestaurantId = id,
                        Category = category

                    };
                    category.RestaurantCategories.Add(resCategory);
                }
            }
            if (category.StoreIds != null)
            {
                category.StoreCategories = new List<StoreCategory>();
                foreach (var id in category.StoreIds)
                {
                    StoreCategory stoCategory = new StoreCategory()
                    {
                        StoreId = id,
                        Category = category

                    };
                    category.StoreCategories.Add(stoCategory);
                }
            }
            if (category.Photo != null)
            {
                if (!category.Photo.CheckFileSize(1000))
                {
                    ModelState.AddModelError("Photo", "Size wrong");
                    return View(category);
                }
                if (!category.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "Type Wrong");
                    return View(category);
                }
                Category newCategory = new Category()
                {
                    Name = category.Name,
                    ImageURL = await category.Photo.SaveFileAsync(_env.WebRootPath, "assets/img"),
                    RestaurantCategories = category.RestaurantCategories,
                    StoreCategories = category.StoreCategories
                };
                await _context.Categories.AddAsync(newCategory);
            }
            else
            {
                Category newCategory = new Category()
                {
                    Name = category.Name,
                    RestaurantCategories = category.RestaurantCategories,
                    StoreCategories = category.StoreCategories
                };
                await _context.Categories.AddAsync(newCategory);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //GET - Update
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.restaurants = _context.Restaurants.ToList();
            ViewBag.stores = _context.Stores.ToList();
            Category category = await _context.Categories.Include(x => x.StoreCategories).Include(x => x.RestaurantCategories).Where(r => r.IsDeleted == false && r.Id == id).FirstOrDefaultAsync();
            if (category.Id != id) return RedirectToAction("Index", "Error");
            category.RestaurantIds = await _context.RestaurantCategories.Select(x => x.RestaurantId).ToListAsync();
            category.StoreIds = await _context.StoreCategories.Select(x => x.StoreId).ToListAsync();
            return View(category);
        }

        //Post - Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Category category)
        {
            if (category.Id != id) return RedirectToAction("Index", "Error");
            Category categoryDb = await _context.Categories.Include(x => x.StoreCategories).Include(x => x.RestaurantCategories).Where(cd => cd.IsDeleted == false && cd.Id == id).FirstOrDefaultAsync();
            if (categoryDb == null) return RedirectToAction("Index", "Error");
            bool isExsistFile = true;
            if (category.Photo == null)
            {
                category.ImageURL = categoryDb.ImageURL;
                isExsistFile = false;
            }
            if (isExsistFile)
            {
                if (!category.Photo.CheckFileSize(1000))
                {
                    ModelState.AddModelError("Photo", "Size wrong");
                    return View(categoryDb);
                }
                if (!category.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "Type Wrong");
                    return View(categoryDb);

                }
                Helper.RemoveFile(_env.WebRootPath, "assets/img", categoryDb.ImageURL);
                string newPhotoName = await category.Photo.SaveFileAsync(_env.WebRootPath, "assets/img");
                categoryDb.ImageURL = newPhotoName;
            }
            categoryDb.Name = category.Name;
            foreach (var restaurantId in category.RestaurantIds.Where(x => !categoryDb.RestaurantCategories.Any(rc => rc.RestaurantId == x)))
            {
                RestaurantCategory restaurantCategory = new RestaurantCategory
                {
                    CategoryId = category.Id,
                    RestaurantId = restaurantId
                };

                categoryDb.RestaurantCategories.Add(restaurantCategory);
            }
            foreach (var storeId in category.StoreIds.Where(x => !categoryDb.StoreCategories.Any(rc => rc.StoreId == x)))
            {
                StoreCategory storeCategory = new StoreCategory
                {
                    CategoryId = category.Id,
                    StoreId = storeId
                };

                categoryDb.StoreCategories.Add(storeCategory);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //GET - Delete
        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.Find(id);
            if (category == null) return RedirectToAction("Index", "Error");
            return View(category);
        }

        //POST - Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            Category dbCategory = await _context.Categories
                                                .Where(c => c.IsDeleted == false && c.Id == id)
                                                .FirstOrDefaultAsync();
            if (dbCategory == null) return RedirectToAction("Index", "Error");
            //_context.Remove(dbCategory);
            dbCategory.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int? Id)
        {
            if (Id == null) return RedirectToAction("Index", "Error");
            ViewBag.Product =await _context.Products.Where(x=>x.CategoryId==Id).ToListAsync();
            Category category = await _context.Categories.Include(c => c.RestaurantCategories).ThenInclude(c => c.Restaurant).Include(c => c.StoreCategories).ThenInclude(c => c.Store).FirstOrDefaultAsync(c => c.Id == Id);
            return View(category);
        }
    }
}
