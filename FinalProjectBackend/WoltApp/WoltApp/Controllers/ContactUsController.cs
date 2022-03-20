using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WoltDataAccess.DAL;
using WoltEntity.Entities;

namespace WoltApp.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ContactUsController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(Message message)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");

            }
            else
            {
                if (!ModelState.IsValid) return View();
                AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
                Message newMessage = new Message
                {
                    AppUserId = appUser.Id,
                    CreatedDate = DateTime.Now,
                    Name=message.Name,
                    Surname=message.Surname,
                    MessageDescription=message.MessageDescription,
                    Email=message.Email
                };
                await _context.Messages.AddAsync(newMessage);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "ContactUs");
        }
    }
}
