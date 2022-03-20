using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoltBusiness.DTOs;
using WoltBusiness.DTOs.Basket;
using WoltDataAccess.DAL;
using WoltEntity.Entities;
using WoltEntity.Utilities.File;

namespace WoltApp.Areas.WoltArea.Controllers
{
    [Area("WoltArea")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 5)
        {
            List<Product> products = await _context.Products
                                                     .Where(p => p.IsDeleted == false)
                                                     .OrderByDescending(p => p.Id)
                                                     .Skip((page - 1) * take)
                                                     .Take(take)
                                                     .Include(p => p.Category)
                                                     .Include(p=>p.RestaurantProducts).ThenInclude(p=>p.Restaurant)
                                                     .Include(p=>p.StoreProducts).ThenInclude(p=>p.Store)
                                                     .ToListAsync();
            var productVms = GetProductList(products);
            int pageCount = GetPageCount(take);
            Paginate<ProductDTO> model = new Paginate<ProductDTO>(productVms, page, pageCount);
            return View(model);
        }
        private int GetPageCount(int take)
        {
            var productCount = _context.Products.Where(p => p.IsDeleted == false).Count();
            return (int)Math.Ceiling(((decimal)productCount / take)+1);
        }
        private List<ProductDTO> GetProductList(List<Product> products)
        {
            List<ProductDTO> model = new List<ProductDTO>();
            foreach (var item in products)
            {
                var product = new ProductDTO
                {
                    Id = item.Id,
                    Title = item.Title,
                    Price = item.Price,
                    Description=item.Description,
                    DiscountPercent=item.DiscountPercent,
                    IsDeleted=item.IsDeleted,
                    IsNew=item.IsNew,
                    CategoryId = item.Category.Id,
                    ImageURL = item.ImageURL
                };
                model.Add(product);
            }
            return model;
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories =await _context.Categories.ToListAsync();
            ViewBag.Restaurants = await _context.Restaurants.ToListAsync();
            ViewBag.Stores = await _context.Stores.ToListAsync();
            return View();
        }

        //POST - Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            ViewBag.Categories = new SelectList(await _context.Categories
                                                             .Where(c => c.IsDeleted == false)
                                                             .ToListAsync(), "Id", "Name");
            ViewBag.Restaurants = await _context.Restaurants.ToListAsync();
            ViewBag.Stores = await _context.Stores.ToListAsync();
            if (!ModelState.IsValid) return View();
            bool isExist = _context.Products.Any(p => p.Title.Trim()
                                                            .ToLower() == product.Title.Trim().ToLower());
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu Product artiq var");
                return View();
            }
            if (!product.Photo.CheckFileSize(1000))
            {
                ModelState.AddModelError("Photo", "Size wrong");
                return View(product);
            }
            if (!product.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Type Wrong");
                return View(product);
            }
            if (product.RestaurantIds != null)
            {
                product.RestaurantProducts = new List<RestaurantProduct>();
                foreach (var id in product.RestaurantIds)
                {
                    RestaurantProduct proRestaurant = new RestaurantProduct()
                    {
                        RestaurantId = id,
                        Product = product

                    };
                    product.RestaurantProducts.Add(proRestaurant);
                }
            }
            if (product.StoreIds != null)
            {
                product.StoreProducts = new List<StoreProduct>();
                foreach (var id in product.RestaurantIds)
                {
                    StoreProduct proStore = new StoreProduct()
                    {
                        StoreId = id,
                        Product = product

                    };
                    product.StoreProducts.Add(proStore);
                }
            }
            Product newProduct = new Product()
            {
                Title = product.Title,
                Description = product.Description,
                Count=product.Count,
                Price=product.Price,
                ImageURL = await product.Photo.SaveFileAsync(_env.WebRootPath, "assets/img"),
                CategoryId=product.CategoryId,
                StoreProducts = product.StoreProducts,
                RestaurantProducts = product.RestaurantProducts
            };
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //GET - Update
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Categories = new SelectList(await _context.Categories
                                                              .Where(c => c.IsDeleted == false)
                                                              .ToListAsync(), "Id", "Name");
            ViewBag.restaurants = _context.Restaurants.ToList();
            ViewBag.stores = _context.Stores.ToList();
            Product product =await _context.Products.Include(p=>p.Category)
                                                    .Include(p=>p.RestaurantProducts)
                                                    .Include(p=>p.StoreProducts)
                                                    .Where(p => p.IsDeleted == false && p.Id == id)
                                                    .FirstOrDefaultAsync();
            if (product == null) return RedirectToAction("Index","Error");
            product.RestaurantIds = await _context.RestaurantProducts.Select(x => x.RestaurantId).ToListAsync();
            product.StoreIds = await _context.StoreProducts.Select(x => x.StoreId).ToListAsync();
            return View(product);
        }

        //POST - Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Product product)
        {
            Product ProductDb = await _context.Products.Include(x => x.RestaurantProducts)
                                                   .ThenInclude(x => x.Restaurant)
                                                   .Include(x => x.StoreProducts)
                                                   .ThenInclude(x => x.Store)
                                                   .FirstOrDefaultAsync(x => x.Id == product.Id);
            if (ProductDb == null) return RedirectToAction("Index", "Error");
            bool isExsistFile = true;
            if (product.Photo == null)
            {
                product.ImageURL = ProductDb.ImageURL;
                isExsistFile = false;
            }
            if (isExsistFile)
            {
                if (!product.Photo.CheckFileSize(1000))
                {
                    ModelState.AddModelError("Photo", "Size wrong");
                    return View(ProductDb);
                }
                if (!product.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "Type Wrong");
                    return View(ProductDb);

                }
                Helper.RemoveFile(_env.WebRootPath, "assets/img", ProductDb.ImageURL);
                string newPhotoName = await product.Photo.SaveFileAsync(_env.WebRootPath, "assets/img");
                ProductDb.ImageURL = newPhotoName;
            }
            setData(ProductDb, product);
            ProductDb.StoreProducts.RemoveAll(x => !product.StoreIds.Contains(x.StoreId));
            ProductDb.RestaurantProducts.RemoveAll(x => !product.RestaurantIds.Contains(x.RestaurantId));
            foreach (var storeId in product.StoreIds.Where(x => !ProductDb.StoreProducts.Any(rc => rc.StoreId == x)))
            {
                StoreProduct storeProduct = new StoreProduct
                {
                    ProductId = product.Id,
                    StoreId = storeId
                };
                ProductDb.StoreProducts.Add(storeProduct);
            }
            foreach (var restaurantId in product.RestaurantIds.Where(x => !ProductDb.RestaurantProducts.Any(rc => rc.RestaurantId == x)))
            {
                RestaurantProduct restaurantProduct = new RestaurantProduct
                {
                    RestaurantId = restaurantId,
                    ProductId = product.Id
                };

                ProductDb.RestaurantProducts.Add(restaurantProduct);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private void setData(Product ProductDb, Product product)
        {
            ProductDb.Title = product.Title;
            ProductDb.Price = product.Price;
            ProductDb.Count = product.Count;
            ProductDb.Description = product.Description;
            ProductDb.Discount = product.Discount;
            ProductDb.IsNew = product.IsNew;
            ProductDb.DiscountPercent = product.DiscountPercent;
        }

        //POST - Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            Product dbproduct = await _context.Products
                                              .Where(p => p.IsDeleted == false && p.Id == id)
                                              .FirstOrDefaultAsync();
            if (dbproduct == null) return RedirectToAction("Index", "Error");
            dbproduct.IsDeleted = true;
            //Helper.RemoveFile(_env.WebRootPath, "assets/img", dbproduct.ImageURL);
            //_context.Products.Remove(dbproduct);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Product");
        }

        //Detail
        public async Task<IActionResult> Detail(int? Id)
        {
            if (Id == null) return RedirectToAction("Index", "Error");
            return View(await _context.Products.Include(x => x.RestaurantProducts).ThenInclude(x => x.Restaurant)
                                               .Include(x => x.StoreProducts).ThenInclude(x => x.Store)
                                               .FirstOrDefaultAsync(c => c.Id == Id));
        }
    }
}
