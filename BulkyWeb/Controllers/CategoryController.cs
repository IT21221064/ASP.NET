
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository db)
        {
            _categoryRepo = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();  
        }
        [HttpPost]
        public IActionResult Create(Category obj)

        {
            //custom validations
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is an invalid valid.");
            }


            if (ModelState.IsValid)
            {
                _categoryRepo.Add(obj);
                _categoryRepo.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) { 
                return NotFound();
            }
            Category categoryFromDb = _categoryRepo.Get(u=>u.Id==id); //can only find using id
           // Category categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id); //can only find using any attribute
           // Category categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault(); //another method
            
            if (categoryFromDb == null) {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)

        {
            //custom validations
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is an invalid valid.");
            }


            if (ModelState.IsValid)
            {
                _categoryRepo.Update(obj);
                _categoryRepo.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryFromDb = _categoryRepo.Get(u => u.Id == id); //can only find using id
            //Category categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id); //can only find using any attribute
           // Category categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault(); //another method

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _categoryRepo.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _categoryRepo.Remove(obj);
            _categoryRepo.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }

}
