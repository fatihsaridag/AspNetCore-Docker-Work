using AspNetCoreMvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFileProvider _fileProvider;
        public HomeController(ILogger<HomeController> logger,IFileProvider fileProvider)
        {
            _logger = logger;
            _fileProvider = fileProvider;
        }

        [HttpGet]
        public IActionResult ImageSave()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ImageSave(IFormFile imageFile)
        {
            if (imageFile!= null && imageFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName); //resim1.jpg
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using(var stream  = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                return View();
            }



            return View();
        }

        [HttpGet]
        public IActionResult ImageShow()
        {
            var images = _fileProvider.GetDirectoryContents("wwwroot/images").ToList().Select(x => x.Name);
            return View(images);
        }

        [HttpPost]
        public IActionResult ImageShow(string name) 
        {
            var file = _fileProvider.GetDirectoryContents("wwwroot/images").ToList().First(x => x.Name == name);
            System.IO.File.Delete(file.PhysicalPath);
            return RedirectToAction("ImageShow");
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}