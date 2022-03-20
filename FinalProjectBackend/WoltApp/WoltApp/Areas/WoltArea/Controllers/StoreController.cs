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

        //GET - Update
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Products = _context.Products.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            Store store = await _context.Stores.Where(r => r.IsDeleted == false && r.Id == id).FirstOrDefaultAsync();
            if (store.Id != id) return RedirectToAction("Index", "Error");
            store.CategoryIds = store.StoreCategories.Select(x => x.CategoryId).ToList();
            store.ProductIds = store.StoreProducts.Select(x => x.ProductId).ToList();
            return View(store);
        }

        //POST - Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Store store)
        {
            if (store.Id != id) return RedirectToAction("Index", "Error");
            Store stoDb = await _context.Stores.Include(x => x.StoreProducts)
                                               .Include(x => x.StoreCategories)
                                               .Where(cd => cd.IsDeleted == false && cd.Id == id)
                                               .FirstOrDefaultAsync();
            if (stoDb == null) return RedirectToAction("Index", "Error");
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
            setData(stoDb, store);
            stoDb.StoreCategories.RemoveAll(x => !store.CategoryIds.Contains(x.CategoryId));
            stoDb.StoreProducts.RemoveAll(x => !store.ProductIds.Contains(x.ProductId));
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
        private void setData(Store dbStore,Store store)
        {
            dbStore.Name = store.Name;
            dbStore.Description = store.Description;
            dbStore.ContactNumber = store.ContactNumber;
            dbStore.Address = store.Address;
            dbStore.Discount = store.Discount;
            dbStore.IsNew = store.IsNew;
            dbStore.DiscountPercent = store.DiscountPercent;
        }

        //POST - Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Store store = await _context.Stores.FindAsync(id);
            if (store == null) return RedirectToAction("Index", "Error");
            store.IsDeleted = true;
            //Helper.RemoveFile(_env.WebRootPath, "assets/img", store.ImageURL);
            //Helper.RemoveFile(_env.WebRootPath, "assets/img", store.HeroImageURL);
            //_context.Stores.Remove(store);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Detail
        public async Task<IActionResult> Detail(int? Id)
        {
            if (Id == null) return RedirectToAction("Index", "Error");
            ViewBag.StoreCategories = await _context.StoreCategories.Include(x => x.Store).Include(x => x.Category).Where(x => x.StoreId == Id).ToListAsync();
            ViewBag.StoreProducts = await _context.StoreProducts.Include(x => x.Product).Where(x => x.StoreId == Id).ToListAsync();
            return View(await _context.Stores.Include(x => x.StoreProducts).ThenInclude(x => x.Product).Include(x => x.StoreCategories).ThenInclude(x => x.Category).FirstOrDefaultAsync(c => c.Id == Id));
        }
    }
}
