using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WoltBusiness.DTOs;
using WoltDataAccess.DAL;

namespace WoltApp.Areas.WoltArea.Controllers
{
    [Area("WoltArea")]
    [Authorize(Roles ="Admin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public DashboardController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            DashboardDTO dashboardDTO = new DashboardDTO
            {
                Sliders = await _context.Sliders.ToListAsync(),
                Restaurants = await _context.Restaurants
                                            .Include(r => r.RestaurantProducts).ThenInclude(r => r.Product)
                                            .ToListAsync(),
                Stores = await _context.Stores.ToListAsync(),
                Products=await _context.Products.ToListAsync(),
                Categories=await _context.Categories.ToListAsync()
            };
            return View(dashboardDTO);
        }
    }
}
