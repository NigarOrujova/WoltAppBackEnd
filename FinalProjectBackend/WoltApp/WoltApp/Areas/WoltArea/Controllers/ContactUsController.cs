using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoltBusiness.DTOs;
using WoltDataAccess.DAL;
using WoltEntity.Entities;

namespace WoltApp.Areas.WoltArea.Controllers
{
    [Area("WoltArea")]
    public class ContactUsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ContactUsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int page = 1,int take=5)
        {
            List<Message> messages = await _context.Messages.OrderByDescending(x => x.CreatedDate)
                                                            .Skip((page - 1) * take)
                                                            .Take(take)
                                                            .ToListAsync();
            var messageVms = GetProductList(messages);
            int pageCount = GetPageCount(take);
            Paginate<ContactUsDTO> model = new Paginate<ContactUsDTO>(messageVms, page, pageCount);
            return View(model);
        }

        private int GetPageCount(int take)
        {
            var messageCount = _context.Messages.Count();
            return (int)Math.Ceiling(((decimal)messageCount / take) + 1);
        }

        private List<ContactUsDTO> GetProductList(List<Message> messages)
        {
            List<ContactUsDTO> model = new List<ContactUsDTO>();
            foreach (var item in messages)
            {
                var message = new ContactUsDTO
                {
                    Id = item.Id,
                    Name=item.Name,
                    Surname=item.Surname,
                    Email=item.Email,
                    Phone=item.Phone,
                    MessageDescription=item.MessageDescription
                };
                model.Add(message);
            }
            return model;
        }
        public async Task<IActionResult> Detail(int? Id)
        {
            if (Id == null) return RedirectToAction("Index", "Error");
            Message message = await _context.Messages.FirstOrDefaultAsync(c => c.Id == Id);
            return View(message);
        }
    }
}
