using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoltDataAccess.DAL;
using WoltEntity.Entities;

namespace WoltApp.Controllers
{
    public class BasketController : Controller
    {
        private UserManager<AppUser> _userManager;
        private AppDbContext _context;

        public BasketController(UserManager<AppUser> userManager
                                ,AppDbContext context)
        {
            _userManager = userManager;
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
            return Json(id);
            //List<BasketDTO> basket = GetBasket();
            //UpdateBasket((int)id, basket);
            //return RedirectToAction("Index", "Home");
        }
    }
}
