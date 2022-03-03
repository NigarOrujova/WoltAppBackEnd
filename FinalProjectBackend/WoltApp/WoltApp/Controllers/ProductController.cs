using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoltBusiness.DTOs.Basket;
using WoltDataAccess.DAL;
using WoltEntity.Entities;

namespace WoltApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id == null) return NotFound();
            Product dbProduct = await _context.Products.FindAsync(id);
            if (dbProduct == null) return BadRequest();
            List<BasketDTO> basket = GetBasket();
            UpdateBasket((int)id, basket);
            return RedirectToAction("Index", "Home");
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
    }
}
