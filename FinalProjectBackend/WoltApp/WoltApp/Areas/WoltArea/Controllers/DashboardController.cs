using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WoltApp.Areas.WoltArea.Controllers
{
    [Area("WoltArea")]
    [Authorize(Roles ="Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
