using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WoltApp.Areas.WoltArea.Controllers
{
    public class RestaurantController : Controller
    {
        [Area("WoltArea")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
