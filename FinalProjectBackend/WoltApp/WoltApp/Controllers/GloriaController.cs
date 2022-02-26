﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoltBusiness.DTOs;
using WoltDataAccess.DAL;

namespace WoltApp.Controllers
{
    public class GloriaController : Controller
    {
        private AppDbContext _context { get; }

        public GloriaController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            RestaurantDTO resDTO = new RestaurantDTO
            {
                RestaurantProducts = await _context.RestaurantProducts.Include(p => p.Restaurant)
                                                                      .Where(p => p.RestaurantId == 12)
                                                                      .Include(p => p.Product).ToListAsync(),
                RestaurantCategories = await _context.RestaurantCategories.Include(c => c.Restaurant)
                                                                          .Where(c => c.RestaurantId == 12)
                                                                          .Include(c => c.Category).ToListAsync(),
                Restaurant = await _context.Restaurants.Where(r => r.IsDeleted == false && r.Id == 12).FirstOrDefaultAsync()
            };
            return View(resDTO);
        }
    }
}
