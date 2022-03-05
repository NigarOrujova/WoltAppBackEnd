using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoltDataAccess.DAL;

namespace WoltApp.ViewComponents
{
    public class UserViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;

        public UserViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
