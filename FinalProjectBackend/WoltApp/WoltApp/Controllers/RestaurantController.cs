using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoltBusiness.DTOs;
using WoltBusiness.DTOs.Basket;
using WoltDataAccess.DAL;
using WoltEntity.Entities;

namespace WoltApp.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        public RestaurantController(UserManager<AppUser> userManager
                                   ,AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index(int Id)
        {
            List<BasketDTO> basket;
            if (Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketDTO>>(Request.Cookies["basket"]);
                ViewBag.BasketItemCount = basket.Count();
            }
            else
            {
                ViewBag.BasketItemCount = 0;
            }
            RestaurantDTO resDTO = new RestaurantDTO
            {
                RestaurantProducts = await _context.RestaurantProducts.Include(p => p.Product).Include(p => p.Restaurant)
                                                          .Where(p => p.RestaurantId == Id)
                                                          .ToListAsync(),
                RestaurantCategories = await _context.RestaurantCategories.Include(c => c.Category).Include(c => c.Restaurant)
                                                              .Where(c => c.RestaurantId == Id)
                                                              .ToListAsync(),
                Restaurant = await _context.Restaurants.Where(r => r.Id==Id).FirstOrDefaultAsync(),
                Comments=await _context.Comments.Where(c=>c.IsDeleted==false && c.RestaurantId == Id).ToListAsync()
            };
            return View(resDTO);
        }

        //GET - Comment
        public async Task<IActionResult> Comment()
        {
            List<Comment> comments = await _context.Comments.OrderByDescending(x=>x.CreatedDate).ToListAsync();
            return View(comments);
        }

        //POST - Comment
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comment(RestaurantDTO restaurantDTO)
        {
            var comment = new Comment()
            {
                Content = restaurantDTO.Content,
                RestaurantId= restaurantDTO.RestaurantId,
                UserName= restaurantDTO.UserName,
                Restaurant=restaurantDTO.Restaurant
            };
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComment(int? Id)
        {
            if (Id == null) return RedirectToAction("Index", "Error");
            Comment comment = await _context.Comments.FindAsync(Id);
            if (comment == null) return RedirectToAction("Index", "Error");
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Comment","Restaurant");
        }
    }
}
