using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoltBusiness.DTOs;
using WoltDataAccess.DAL;
using WoltEntity.Entities;

namespace WoltApp.Controllers
{
    public class McController : Controller
    {
        private AppDbContext _context { get; }

        public McController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            //RestaurantDTO resDTO = new RestaurantDTO
            //{
            //    Products = await _context.Products.Where(c => c.IsDeleted == false)
            //                                      .Include(p => p.RestaurantProducts)
            //                                      .ThenInclude(p=>p.Restaurant)
            //                                      .ToListAsync(),
            //    Categories = await _context.Categories
            //    .Where(c => c.IsDeleted == false && c.ImageURL != null)
            //    .ToListAsync(),
            //    Restaurants = await _context.Restaurants.Where(r => r.IsDeleted == false && r.Id==1).Include(r => r.RestaurantProducts).ThenInclude(r => r.Product).ToListAsync()
            //};
            List<RestaurantProduct> restaurant = await _context.RestaurantProducts.Include(p => p.Restaurant).Where(p=>p.RestaurantId==1).Include(p=>p.Product).ToListAsync();
            return View(restaurant);
        }
    }
}
