using Microsoft.AspNetCore.Mvc;

namespace WoltApp.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
