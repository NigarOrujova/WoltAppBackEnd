using Microsoft.AspNetCore.Identity;
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
    public class StoreController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        public StoreController(UserManager<AppUser> userManager
                                   , AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index(int Id)
        {
            StoreDTO resDTO = new StoreDTO
            {
                StoreProducts = await _context.StoreProducts.Include(p => p.Store)
                                                          .Where(p => p.StoreId == Id)
                                                          .Include(p => p.Product).ToListAsync(),
                StoreCategories = await _context.StoreCategories.Include(p => p.Store)
                                                          .Where(p => p.StoreId == Id)
                                                              .Include(c => c.Category).ToListAsync(),
                Store = await _context.Stores.Where(r =>r.Id == Id).FirstOrDefaultAsync()
            };
            return View(resDTO);
        }
    }
}
