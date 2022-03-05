using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WoltBusiness.DTOs.Basket;
using WoltDataAccess.DAL;
using WoltEntity.Entities;

namespace WoltBusiness.Services
{
    public class LayoutServices
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public LayoutServices(AppDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }
        public List<Product> GetBasketItems()
        {
            var ItemsDb = _httpContext.HttpContext.Request.Cookies["basket"];
            List<Product> items = new List<Product>();
            if (ItemsDb != null)
            {
                items = JsonConvert.DeserializeObject<List<Product>>(ItemsDb);
            }
            return items;
        }

    }
}
