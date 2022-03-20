using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
    public class CommentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CommentController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 5)
        {
            List<Comment> comments = await _context.Comments.Where(x=>x.IsDeleted==false)
                                                            .OrderByDescending(x => x.CreatedDate)
                                                            .Skip((page - 1) * take)
                                                            .Take(take)
                                                            .ToListAsync();
            var commentVms = GetProductList(comments);
            int pageCount = GetPageCount(take);
            Paginate<CommentDTO> model = new Paginate<CommentDTO>(commentVms, page, pageCount);
            return View(model);
        }
        private int GetPageCount(int take)
        {
            var commentCount = _context.Comments.Count();
            return (int)Math.Ceiling(((decimal)commentCount / take) + 1);
        }
        private List<CommentDTO> GetProductList(List<Comment> comments)
        {
            List<CommentDTO> model = new List<CommentDTO>();
            foreach (var item in comments)
            {
                var comment = new CommentDTO
                {
                    Id = item.Id,
                    UserName=item.UserName,
                    Content=item.Content,
                    CreatedDate=DateTime.Now
                };
                model.Add(comment);
            }
            return model;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null) return RedirectToAction("Index", "Error");
            Comment comment = await _context.Comments.FindAsync(Id);
            if (comment == null) return RedirectToAction("Index", "Error");
            comment.IsDeleted = true;
            //_context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Comment");
        }
        public async Task<IActionResult> Detail(int? Id)
        {
            if (Id == null) return RedirectToAction("Index", "Error");
            Comment comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == Id);
            return View(comment);
        }
    }
}
