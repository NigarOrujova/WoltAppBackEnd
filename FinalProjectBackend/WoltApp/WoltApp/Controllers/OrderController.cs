using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoltDataAccess.DAL;
using WoltEntity.Entities;

namespace WoltApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _env;
        public OrderController(AppDbContext context, UserManager<AppUser> userManager,
            IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyOrders()
        {
            List<FullOrder> fullOrders = _context.FullOrders.Include(x => x.Orders).Where(x => x.AppUser.UserName == User.Identity.Name).ToList();
            return View(fullOrders);
        }

        public async Task<IActionResult> PleaceOrder()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            if (!ModelState.IsValid) return RedirectToAction("Index", "Error");
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            List<BasketItem> basketItems = await _context.BasketItems
                                                         .Include(b => b.Product)
                                                         .Where(b => b.IsDeleted == false && b.AppUserId == appUser.Id)
                                                         .ToListAsync();
            if (basketItems.Count == 0) return RedirectToAction("Index", "Error");
            FullOrder order = new FullOrder
            {
                AppUserId = appUser.Id,
                CreatedDate = DateTime.UtcNow
            };
            List<Order> orderItems = new List<Order>();
            double total = 0;
            foreach (BasketItem item in basketItems)
            {
                Order orderItem = new Order
                {
                    Count = item.Count,
                    Price = (double)item.Price,
                    ProductId = item.ProductId,
                };
                total += (orderItem.Count * (double)orderItem.Price);
                orderItems.Add(orderItem);
                await _context.Orders.AddAsync(orderItem);
                _context.BasketItems.Remove(item);
            }
            order.TotalCount = total;
            order.Orders = orderItems;
            await _context.FullOrders.AddAsync(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("MyOrders","Order");
        }
    }
}
