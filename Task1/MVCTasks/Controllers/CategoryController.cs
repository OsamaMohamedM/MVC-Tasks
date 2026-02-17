using Microsoft.AspNetCore.Mvc;
using MVCTasks.Context;
using MVCTasks.Models;

namespace MVCTasks.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext db;

        public CategoryController(AppDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Id = Guid.NewGuid();
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Edit(Guid id)
        {
            var category = db.Categories.Find(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit([Bind("Id,Name,Description")] Category model)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Update(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Category/Delete/5
        public IActionResult Delete(Guid id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var category = db.Categories.Find(id);
            if (category != null)
            {
                db.Categories.Remove(category);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}