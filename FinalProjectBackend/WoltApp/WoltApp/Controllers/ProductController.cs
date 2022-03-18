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
            List<BasketDTO> basketItems = new List<BasketDTO>();
            AppUser user = User.Identity.IsAuthenticated ? await _userManager.FindByNameAsync(User.Identity.Name) : null;

            if (!User.Identity.IsAuthenticated)
            {
                if (HttpContext.Request.Cookies["Basket"] != null)
                {
                    basketItems = JsonConvert.DeserializeObject<List<BasketDTO>>(HttpContext.Request.Cookies["Basket"]);
                }
            }
            else
            {

                basketItems = await _context.BasketItems.Where(x => x.AppUserId == user.Id && x.IsDeleted == false).Select(x => new BasketDTO
                {
                    Count = x.Count,
                    ImageURL = x.Product.ImageURL,
                    Title = x.Product.Title,
                    Price = x.Product.Price,
                    DiscountPercent=x.Product.DiscountPercent

                }).ToListAsync();
            }

            return View(basketItems);
        }

        //User Add Basket
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

        //Cookie
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Error");
            Product dbProduct = await _context.Products.FindAsync(id);
            if (dbProduct == null) return RedirectToAction("Index", "Error");
            List<BasketDTO> basket = GetBasket();
            UpdateBasket((int)id, basket);
            return RedirectToAction("Index", "Home");
        }

        //Cookie
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

        //cookie
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

        //cookie
        public async Task<IActionResult> Basket()
        {
            List<BasketDTO> basket = GetBasket();
            List<BasketDTO> model = await GetBasketList(basket);
            return View(model);
        }

        //cookie
        public async Task<List<BasketDTO>> GetBasketList(List<BasketDTO> basket)
        {
            List<BasketDTO> model = new List<BasketDTO>();
            foreach (BasketDTO item in basket)
            {
                Product DbProduct = await _context.Products.Include(p => p.Category).Include(p => p.RestaurantProducts).ThenInclude(p => p.Restaurant)
                                                    .FirstOrDefaultAsync(p => p.Id == item.Id);
                BasketDTO itemDTO = GetBasketItem(item, DbProduct);
                model.Add(itemDTO);
            }
            return model;
        }

        //cookie
        private BasketDTO GetBasketItem(BasketDTO item, Product DbProduct)
        {
            return new BasketDTO
            {
                Id = item.Id,
                Title = DbProduct.Title,
                Count = item.Count,
                StockCount = DbProduct.Count,
                Catagory = DbProduct.Category.Name,
                ImageURL = DbProduct.ImageURL,
                Price = DbProduct.Price,
                DiscountPercent = DbProduct.DiscountPercent,
                IsActive = DbProduct.IsDeleted
            };
        }

        //User basket
        public async Task<IActionResult> ShowBasketItems()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            List<BasketItem> basket = GetBasket(user.Id);
            List<BasketItemDTO> model = await GetBasketList(basket);
            UserBasketDTO userBasket = new UserBasketDTO
            {
                basketItemDTOs = model
            };
            return View(userBasket);

        }

        //userbasket
        private async Task<List<BasketItemDTO>> GetBasketList(List<BasketItem> basket)
        {
            List<BasketItemDTO> model = new List<BasketItemDTO>();
            foreach (BasketItem item in basket)
            {
                Product dbProduct = await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == item.ProductId);
                BasketItemDTO basketItemDTO = getBasketItem(item, dbProduct);
                model.Add(basketItemDTO);
            }
            return model;
        }

        //user basket
        private List<BasketItem> GetBasket(string userId)
        {
            var basket = _context.BasketItems.Where(x => x.AppUserId == userId).ToList();
            return basket;
        }

        //user basket 
        private BasketItemDTO getBasketItem(BasketItem item, Product DbProduct)
        {
            return new BasketItemDTO
            {
                Id = item.Id,
                Title = DbProduct.Title,
                Count = item.Count,
                StockCount = DbProduct.Count,
                Catagory = DbProduct.Category.Name,
                ImageURL = DbProduct.ImageURL,
                Price = DbProduct.Price,
                DiscountPercent = DbProduct.DiscountPercent,
                IsActive = DbProduct.IsDeleted
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmptyBasket()
        {
            AppUser user = User.Identity.IsAuthenticated ? await _userManager.FindByNameAsync(User.Identity.Name) : null;

            if (!User.Identity.IsAuthenticated)
            {
                Response.Cookies.Delete("basket");
            }
            else
            {
                List<BasketItem> basketItem = await _context.BasketItems.Where(x => x.AppUserId == user.Id && x.IsDeleted == false).ToListAsync();
                _context.BasketItems.RemoveRange(basketItem);
                _context.SaveChanges();

            }
            return RedirectToAction("Index","Product");

        }
        public async Task<IActionResult> RemoveProductFromBasket(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Error");
            List<BasketDTO> productBaskets = new List<BasketDTO>();
            AppUser user = User.Identity.IsAuthenticated ? await _userManager.FindByNameAsync(User.Identity.Name) : null;
            if (User.Identity.IsAuthenticated)
            {
                BasketItem dbBasketItem = _context.BasketItems.FirstOrDefault(x => x.AppUserId == user.Id && x.Id == id && x.IsDeleted == false);
                if (dbBasketItem == null) return RedirectToAction("Index", "Error");
                    if (dbBasketItem.Count == 1 || dbBasketItem.Count <= 0)
                {

                    _context.BasketItems.Remove(dbBasketItem);
                }
                else
                {
                    dbBasketItem.Count--;
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("ShowBasketItems", "Product");
            }
            else
            {
                string basketItms = HttpContext.Request.Cookies["basket"];
                productBaskets = JsonConvert.DeserializeObject<List<BasketDTO>>(basketItms);
                BasketDTO BasketProduct = productBaskets.FirstOrDefault(p => p.Id == id);
                if (BasketProduct == null) return RedirectToAction("Index", "Error");
                if (BasketProduct.Count == 1 || BasketProduct.Count <= 0)
                {
                    productBaskets.Remove(BasketProduct);
                }
                else
                {
                    BasketProduct.Count--;
                }
                HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(productBaskets));
                return RedirectToAction("Basket", "Product");
            }
            return View();
        }
    }
}
