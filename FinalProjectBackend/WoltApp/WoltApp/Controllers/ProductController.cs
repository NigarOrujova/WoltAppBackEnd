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
                    Title = x.Product.Title,
                    Price = x.Product.Price

                }).ToListAsync();
            }

            return View(basketItems);
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

        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id == null) return NotFound();
            Product dbProduct = await _context.Products.FindAsync(id);
            if (dbProduct == null) return BadRequest();
            List<BasketDTO> basket = GetBasket();
            UpdateBasket((int)id, basket);
            return RedirectToAction("Index", "Home");
        }
        public List<BasketDTO> GetBasket()
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
        public void UpdateBasket(int productid, List<BasketDTO> basket)
        {
            BasketDTO BasketProduct = basket.Find(p => p.Id == productid);
            if (BasketProduct == null)
            {
                basket.Add(new BasketDTO
                {
                    Id = productid,
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
        public  async Task<List<BasketItemDTO>> GetBasketList(List<BasketDTO> basket)
        {
            List<BasketItemDTO> model = new List<BasketItemDTO>();
            foreach (BasketDTO item in basket)
            {
                Product DbProduct = await _context.Products.Include(p=>p.Category).Include(p=>p.RestaurantProducts).ThenInclude(p=>p.Restaurant)
                                                    .FirstOrDefaultAsync(p => p.Id == item.Id);
                BasketItemDTO itemDTO = getBasketItem(item, DbProduct);
                model.Add(itemDTO);
            }
            return model;
        }
        private BasketItemDTO getBasketItem(BasketDTO item, Product DbProduct)
        {
            return new BasketItemDTO
            {
                Title = DbProduct.Title,
                Count = item.Count,
                StockCount = DbProduct.Count,
                Catagory=DbProduct.Category.Name,
                ImageURL = DbProduct.ImageURL,
                Price = DbProduct.Price,
                IsActive = DbProduct.IsDeleted
            };
        }
        public async Task<IActionResult> ShowBasketItems()
        {
            var userId = _userManager.GetUserId(User);

            var ProductIds =await _context.BasketItems
                    .Where(f => f.AppUserId == userId)
                    .Select(m => m.ProductId)
                    .Distinct()
                    .ToListAsync();

            var basketItem =await _context.Products
                                .Where(m => ProductIds.Contains(m.Id))
                                .ToListAsync();

            return View(basketItem);

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
