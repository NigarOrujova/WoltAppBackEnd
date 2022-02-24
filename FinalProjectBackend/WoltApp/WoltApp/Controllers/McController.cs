using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoltBusiness.DTOs;
using WoltDataAccess.DAL;

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
            RestaurantDTO resDTO = new RestaurantDTO
            {
                Products = await _context.Products.Where(c => c.IsDeleted == false).ToListAsync(),
                Categories = await _context.Categories
                .Where(c => c.IsDeleted == false && c.ImageURL != null)
                .ToListAsync(),
                Restaurants = await _context.Restaurants.Where(r => r.IsDeleted == false).ToListAsync()
            };
            return View(resDTO);
        }
    }
}
