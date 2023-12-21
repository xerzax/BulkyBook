using BulkyBookWeb.Data;
using BulkyBookWeb.Interface;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller

	{
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [Authorize]
        public IActionResult Index()
		{
            var returnedCategory = _categoryRepository.GetAllCategory().Where(x=>x.isDeleted== false);
            return View(returnedCategory);
		}
        //GET
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The display order cant exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _categoryRepository.AddCategory(obj);
                TempData["success"] = "Category created successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);   
            
        }
        //GET
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _categoryRepository.GetCategoryById(id);
            if(categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        //POST
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The display order cant exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _categoryRepository.UpdateCategory(obj);
                TempData["success"] = "Category updated successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //GET
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _categoryRepository.GetCategoryById(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int id)
        {
            var deletedCategory = _categoryRepository.DeleteCategory(id);
            if(deletedCategory) {
                TempData["success"] = "Category deleted successfully!";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");



        }
    }
}
