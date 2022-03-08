using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WoltBusiness.DTOs.Basket;
using WoltDataAccess.DAL;
using WoltEntity.Entities;

namespace WoltApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ProductController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            List<BasketItemDTO> basketItems = new List<BasketItemDTO>();
            AppUser user = User.Identity.IsAuthenticated ? await _userManager.FindByNameAsync(User.Identity.Name) : null;

            if (!User.Identity.IsAuthenticated)
            {
                if (HttpContext.Request.Cookies["Basket"] != null)
                {
                    basketItems = JsonConvert.DeserializeObject<List<BasketItemDTO>>(HttpContext.Request.Cookies["Basket"]);
                }
            }
            else
            {

                basketItems = await _context.BasketItems.Where(x => x.AppUserId == user.Id && x.IsDeleted == false).Select(x => new BasketItemDTO
                {
                    Count = x.Count,
                    ImageURL = x.Product.ImageURL,
                    ProductId = x.ProductId,
                    Title = x.Product.Title,
                    Price = x.Product.Price

                }).ToListAsync();
            }

            return View(basketItems);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id == null) return NotFound();
            Product dbProduct = await _context.Products.FindAsync(id);
            if (dbProduct == null) return BadRequest();
            List<BasketDTO> basket = GetBasket();
            UpdateBasket((int)id, basket);
            return RedirectToAction("Index", "Home");
        }
        public async Task AddBasketItem(int productid)
        {
            ClaimsPrincipal currentUser = User;
            var userId = _userManager.GetUserId(User);

            BasketItem basketItem = _context.BasketItems.Where(b => b.AppUserId == userId && b.ProductId == productid).FirstOrDefault();

            if (userId != null)
            {

                if (basketItem == null)
                {
                    basketItem = new BasketItem
                    {
                        AppUserId = userId,
                        ProductId = productid,
                        Count = 1
                    };
                    await _context.BasketItems.AddAsync(basketItem);

                }
                else
                {
                    basketItem.Count += 1;
                }

            }


            await _context.SaveChangesAsync();
        }
        private List<BasketDTO> GetBasket()
        {
            List<BasketDTO> basket;
            if (Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketDTO>>(Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketDTO>();
            }
            return basket;
        }
        private void UpdateBasket(int id, List<BasketDTO> basket)
        {
            BasketDTO BasketProduct = basket.Find(p => p.Id == id);
            if (BasketProduct == null)
            {
                basket.Add(new BasketDTO
                {
                    Id = id,
                    Count = 1
                });
            }
            else
            {
                BasketProduct.Count += 1;
            }
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
        }
        public async Task<IActionResult> Basket()
        {
            List<BasketDTO> basket = GetBasket();
            List<BasketItemDTO> model = await GetBasketList(basket);
            return View(model);
        }
        private async Task<List<BasketItemDTO>> GetBasketList(List<BasketDTO> basket)
        {
            List<BasketItemDTO> model = new List<BasketItemDTO>();
            foreach (BasketDTO item in basket)
            {
                Product DbProduct = await _context.Products.Include(p=>p.Category).Include(p=>p.RestaurantProducts).ThenInclude(p=>p.Restaurant)
                                                    .FirstOrDefaultAsync(p => p.Id == item.Id);
                BasketItemDTO itemDTO = getBAsketItem(item, DbProduct);
                model.Add(itemDTO);
            }
            return model;
        }
        //private async Task<List<BasketItemDTO>> GetBasketList(List<BasketItem> basketItem)
        //{
        //    List<BasketItemDTO> model = new List<BasketItemDTO>();
        //    foreach (var item in basketItem)
        //    {
        //        BasketItemDTO itemDTO = getBAsketItem(item);
        //        model.Add(itemDTO);
        //    }
        //    return model;
        //}
        private BasketItemDTO getBAsketItem(BasketDTO item, Product DbProduct)
        {
            return new BasketItemDTO
            {
                Id = item.Id,
                Title = DbProduct.Title,
                Count = item.Count,
                StockCount = DbProduct.Count,
                Catagory=DbProduct.Category.Name,
                ImageURL = DbProduct.ImageURL,
                Price = DbProduct.Price,
                IsActive = DbProduct.IsDeleted
            };
        }
        //private BasketItemDTO getBAsketItem(BasketItem item)
        //{
        //    return new BasketItemDTO
        //    {
        //        Id = item.Id,
        //        Title = item.Product.Title,
        //        Count = item.Count,
        //        StockCount = item.Product.Count,
        //        Catagory = item.Product.Category.Name,
        //        ImageURL = item.Product.ImageURL,
        //        Price = item.Product.Price,
        //        IsActive = item.Product.IsDeleted
        //    };
        //}
        //public IActionResult RemoveProcuctFromBasket(int? id)
        //{
        //    if (id == null) return NotFound();
        //    List<BasketItemDTO> model = new List<BasketItemDTO>();
        //    List<BasketItemDTO> basket = GetBasketItem();
        //    RemoveProduct((int)id, basket);
        //    return RedirectToAction("basket");
        //}
        //private List<BasketItemDTO> GetBasketItem()
        //{
        //    List<BasketItemDTO> basket;
        //    if (Request.Cookies["basket"] != null)
        //    {
        //        basket = JsonConvert.DeserializeObject<List<BasketItemDTO>>(Request.Cookies["basket"]);
        //    }
        //    else
        //    {
        //        basket = new List<BasketItemDTO>();
        //    }
        //    return basket;
        //}
        //public IActionResult RemoveProduct(int id, List<BasketItemDTO> basket)
        //{
        //    BasketItemDTO BasketProduct = basket.FirstOrDefault(x=>x.ProductId==id);
        //    if (BasketProduct == null) return NotFound();
        //    if (BasketProduct.Count != 1 || BasketProduct.Count <= 0)
        //    {
        //        BasketProduct.Count--;
        //    }
        //    Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
        //    return RedirectToAction(nameof(Index));
        //}

        public IActionResult ShowBasketItem()
        {
            string productshowitem = HttpContext.Request.Cookies["basket"];
            return Content(productshowitem);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmptyBasket()
        {
            AppUser user = User.Identity.IsAuthenticated ? await _userManager.FindByNameAsync(User.Identity.Name) : null;

            if (!User.Identity.IsAuthenticated)
            {
                Response.Cookies.Delete("Basket");
            }
            else
            {
                List<BasketItem> basketItem = await _context.BasketItems.Where(x => x.AppUserId == user.Id && x.IsDeleted == false).ToListAsync();
                _context.BasketItems.RemoveRange(basketItem);
                _context.SaveChanges();

            }
            return RedirectToAction("Index","Product");

        }
    }
}
