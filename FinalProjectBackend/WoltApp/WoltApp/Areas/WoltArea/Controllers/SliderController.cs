using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WoltBusiness.DTOs;
using WoltDataAccess.DAL;
using WoltEntity.Entities;
using WoltEntity.Utilities.File;

namespace WoltApp.Areas.WoltArea.Controllers
{
    [Area("WoltArea")]
    [Authorize(Roles = "Admin")]
    public class SliderController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;
        private string _errorMessage;
        private string _errorMessageCount;

        private Dictionary<string, string> SliderCount { get; set; }
        private Dictionary<string, string> ImageSize { get; set; }

        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
            SliderCount = _context.Settings.AsEnumerable().ToDictionary(s => s.Key, s => s.Value);
            ImageSize = _context.Settings.AsEnumerable().ToDictionary(s => s.Key, s => s.Value);
        }
        public IActionResult Index()
        {
            return View(_context.Sliders);
        }

        //GET - Create
        public IActionResult Create()
        {
            return View();
        }

        //POST - Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MultipleSliderDTO sliderDTO)
        {

            if (ModelState["Photos"].ValidationState == ModelValidationState.Invalid) return View();
            if (!CheckImageValid(sliderDTO.Photos))
            {
                ModelState.AddModelError("Photos", _errorMessage);
                return View();
            }
            if (!CheckImageCount(sliderDTO.Photos))
            {
                ModelState.AddModelError("Photos", _errorMessageCount);
                return View();
            }
            foreach(var photo in sliderDTO.Photos)
            {
                string fileName = await photo.SaveFileAsync(_env.WebRootPath, "assets/img");
                Slider slider = new Slider
                {
                    ImageURL = fileName,
                    Title= sliderDTO.Title,
                    Description= sliderDTO.Description
                };
                await _context.Sliders.AddAsync(slider);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool CheckImageCount(List<IFormFile> photos)
        {
            int maxImageUpload = int.Parse(SliderCount["sCount"]),
               dbSliderImageCount = _context.Sliders.Count()- maxImageUpload,
               upload = maxImageUpload - dbSliderImageCount;
            if (dbSliderImageCount == maxImageUpload)
            {
                _errorMessageCount = $"Slider-də şəkil sayı maximumdur";
                return false;
            }
            if (!(photos.Count() <= upload))
            {
                _errorMessageCount = $"Slider-a {maxImageUpload}-dən artıq şəkil yükləmək olmaz";
                return false;
            }
            return true;
        }
        private bool CheckImageValid(List<IFormFile> photos)
        {
            foreach (var photo in photos)
            {
                if (!photo.CheckFileType("image/"))
                {
                    _errorMessage = $"{photo.FileName} should be image type";
                    return false;
                }
                if (!photo.CheckFileSize(int.Parse(ImageSize["fileSize"])))
                {
                    _errorMessage = $"{photo.FileName}-Faylın ölçüsü 1000kbdan çox ola bilməz";
                    return false;
                }
            }
            return true;
        }

        //POST - Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Slider slider = await _context.Sliders.FindAsync(id);
            if (slider == null) return RedirectToAction("Index", "Error");
            Helper.RemoveFile(_env.WebRootPath, "assets/img", slider.ImageURL);
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //GET - Update
        public async Task<IActionResult> Update(int id)
        {
            Slider slider = await _context.Sliders.FindAsync(id);
            return View(slider);
        }

        //POST - Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Slider slider)
        {
            if (id != slider.Id) return RedirectToAction("Index", "Error");
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return RedirectToAction(nameof(Index));
            Slider dbSlider = await _context.Sliders.FindAsync(id);
            if (dbSlider == null) return RedirectToAction("Index", "Error");
            if (!slider.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "File should be image type");
                return View();
            }
            if (!slider.Photo.CheckFileSize(int.Parse(ImageSize["fileSize"])))
            {
                ModelState.AddModelError("Photo", "Faylın ölçüsü 1000kbdan çox ola bilməz");
                return View();
            }
            Helper.RemoveFile(_env.WebRootPath, "assets/img", dbSlider.ImageURL);
            string newFileName = await slider.Photo.SaveFileAsync(_env.WebRootPath, "assets/img");
            dbSlider.ImageURL = newFileName;
            dbSlider.Title = slider.Title;
            dbSlider.Description = slider.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Detail
        public async Task<IActionResult> Detail(int? Id)
        {
            if (Id == null) return RedirectToAction("Index", "Error");
            return View(await _context.Sliders.FirstOrDefaultAsync(x => x.Id == Id));
        }
    }
}
