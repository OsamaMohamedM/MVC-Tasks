using Model_Binding_Sources__FileUploading.Models;

namespace Model_Binding_Sources__FileUploading.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.StaticFiles;
    using System.IO;

    public class HomeController : Controller
    {
        private readonly string _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "InternalStorage", "Reports");

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] ReportUploadViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Directory.CreateDirectory(_storagePath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ReportFile.FileName);
            var fullPath = Path.Combine(_storagePath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await model.ReportFile.CopyToAsync(stream);

            return RedirectToAction("Index");
        }

        public IActionResult Download(string? fileName)
        {
            if (fileName == null) return View();
            var safeFileName = Path.GetFileName(fileName);
            var filePath = Path.Combine(_storagePath, safeFileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return PhysicalFile(filePath, contentType, safeFileName);
        }
    }
}