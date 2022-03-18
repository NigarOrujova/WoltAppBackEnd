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
    public class StoreController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;

        public StoreController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 5)
        {
            List<Store> stores = await _context.Stores
                                               .OrderByDescending(p => p.Id)
                                               .Skip((page - 1) * take)
                                               .Take(take)
                                             .Include(x => x.StoreCategories).ThenInclude(x => x.Category)
                                             .Include(x => x.StoreProducts).ThenInclude(x => x.Product)
                                             .ToListAsync();
            var storeVms = GetStoresList(stores);
            int pageCount = GetPageCount(take);
            Paginate<StoreDTO> model = new Paginate<StoreDTO>(storeVms, page, pageCount);
            return View(model);
        }
        private int GetPageCount(int take)
        {
            var storeCount = _context.Stores.Count();
            return (int)Math.Ceiling(((decimal)storeCount / take)+1);
        }
        private List<StoreDTO> GetStoresList(List<Store> stores)
        {
            List<StoreDTO> model = new List<StoreDTO>();
            foreach (var item in stores)
            {
                var store  = new StoreDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Address = item.Address,
                    ContactNumber = item.ContactNumber,
                    DiscountPercent = item.DiscountPercent,
                    IsDeleted = item.IsDeleted,
                    IsNew = item.IsNew,
                    ImageURL = item.ImageURL,
                    HeroImageURL = item.HeroImageURL
                };
                model.Add(store);
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
        public async Task<IActionResult> Create(Store store)
        {
            ViewBag.Products = _context.Products.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            if (!ModelState.IsValid) return View(store);
            store.StoreProducts = new List<StoreProduct>();
            store.StoreCategories = new List<StoreCategory>();
            if (store.CategoryIds != null)
            {
                foreach (var id in store.CategoryIds)
                {
                    StoreCategory stoCategory = new StoreCategory()
                    {
                        CategoryId = id,
                         Store= store

                    };
                    store.StoreCategories.Add(stoCategory);
                }
            }
            if (store.ProductIds != null)
            {
                foreach (var id in store.ProductIds)
                {
                    StoreProduct stoProduct = new StoreProduct()
                    {
                        ProductId = id,
                        Store = store

                    };
                    store.StoreProducts.Add(stoProduct);
                }
            }
            if (!store.HeroPhoto.CheckFileSize(1000))
            {
                ModelState.AddModelError("HeroPhoto", "Size wrong");
                return View(store);
            }
            if (!store.HeroPhoto.CheckFileType("image/"))
            {
                ModelState.AddModelError("HeroPhoto", "Type Wrong");
                return View(store);
            }
            if (!store.Photo.CheckFileSize(1000))
            {
                ModelState.AddModelError("Photo", "Size wrong");
                return View(store);
            }
            if (!store.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Type Wrong");
                return View(store);
            }
            Store newStore = new Store()
            {
                Name = store.Name,
                Description = store.Description,
                Address = store.Address,
                ContactNumber = store.ContactNumber,
                ImageURL = await store.Photo.SaveFileAsync(_env.WebRootPath, "assets/img"),
                HeroImageURL = await store.HeroPhoto.SaveFileAsync(_env.WebRootPath, "assets/img"),
                StoreCategories = store.StoreCategories,
                StoreProducts = store.StoreProducts,
                IsNew = true
            };

            await _context.Stores.AddAsync(newStore);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Products = _context.Products.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            Store store = await _context.Stores.Where(r => r.IsDeleted == false && r.Id == id).FirstOrDefaultAsync();
            if (store.Id != id) return BadRequest();
            return View(store);
        }

        //POST - Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Store store)
        {
            if (store.Id != id) return BadRequest();
            Store stoDb = await _context.Stores.Include(x => x.StoreProducts).Include(x => x.StoreCategories).Where(cd => cd.IsDeleted == false && cd.Id == id).FirstOrDefaultAsync();
            if (stoDb == null) return NotFound();
            if (store.Description == null) return View(stoDb);
            bool isExsistFile = true;
            if (store.Photo == null && store.HeroPhoto == null)
            {
                store.ImageURL = stoDb.ImageURL;
                store.HeroImageURL = stoDb.HeroImageURL;
                isExsistFile = false;
            }
            if (isExsistFile)
            {
                if (!store.Photo.CheckFileSize(1000))
                {
                    ModelState.AddModelError("Photo", "Size wrong");
                    return View(stoDb);
                }
                if (!store.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "Type Wrong");
                    return View(stoDb);

                }
                if (!store.HeroPhoto.CheckFileSize(1000))
                {
                    ModelState.AddModelError("HeroPhoto", "Size wrong");
                    return View(store);
                }
                if (!store.HeroPhoto.CheckFileType("image/"))
                {
                    ModelState.AddModelError("HeroPhoto", "Type Wrong");
                    return View(store);
                }
                Helper.RemoveFile(_env.WebRootPath, "assets/img", stoDb.ImageURL);
                string newPhotoName = await store.Photo.SaveFileAsync(_env.WebRootPath, "assets/img");
                stoDb.ImageURL = newPhotoName;
                Helper.RemoveFile(_env.WebRootPath, "assets/img", stoDb.HeroImageURL);
                string newHeroPhotoName = await store.HeroPhoto.SaveFileAsync(_env.WebRootPath, "assets/img");
                stoDb.HeroImageURL = newHeroPhotoName;
            }
            stoDb.Name = store.Name;
            stoDb.Description = store.Description;
            stoDb.ContactNumber = store.ContactNumber;
            stoDb.Address = store.Address;
            stoDb.Discount = store.Discount;
            stoDb.IsNew = store.IsNew;
            stoDb.DiscountPercent = store.DiscountPercent;
            foreach (var categoryId in store.CategoryIds.Where(x => !stoDb.StoreCategories.Any(rc => rc.CategoryId == x)))
            {
                StoreCategory storeCategory = new StoreCategory
                {
                    StoreId = store.Id,
                    CategoryId = categoryId
                };

                stoDb.StoreCategories.Add(storeCategory);
            }
            foreach (var productId in store.ProductIds.Where(x => !stoDb.StoreProducts.Any(rc => rc.ProductId == x)))
            {
                StoreProduct storeProduct = new StoreProduct
                {
                    StoreId = store.Id,
                    ProductId = productId
                };

                stoDb.StoreProducts.Add(storeProduct);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //POST - Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Store store = await _context.Stores.FindAsync(id);
            if (store == null) return NotFound();
            //store.IsDeleted = true;
            Helper.RemoveFile(_env.WebRootPath, "assets/img", store.ImageURL);
            Helper.RemoveFile(_env.WebRootPath, "assets/img", store.HeroImageURL);
            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
