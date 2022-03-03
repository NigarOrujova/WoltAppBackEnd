using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Basket()
        {
            return Json(JsonConvert.DeserializeObject<List<BasketDTO>>(Request.Cookies["basket"]));
        }
    }
}
