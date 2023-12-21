using BulkyBookWeb.Data;
using BulkyBookWeb.Interface;
using BulkyBookWeb.Models;
using BulkyBookWeb.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICommentRepository _commentRepository;
		

		public BookController(IBookRepository bookRepository, ICategoryRepository categoryRepository, IWebHostEnvironment webHostEnvironment, ICommentRepository commentRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepo = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
            _commentRepository = commentRepository;
			
		}

        [Authorize]
        public IActionResult Index()
        {
            var books = _bookRepository.GetAllBooks().Where(x=>x.IsDeleted == false);
            return View(books);
        }

        // GET
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_categoryRepo.GetAllCategory(), "Id", "Name");
            return View();
        }

        // POST
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Book obj)
        {
            var image = Request.Form.Files.FirstOrDefault();
            if (image != null)
            {
                var fileName = Guid.NewGuid().ToString();
                var path = $@"images\";
                var wwwRootPath = _webHostEnvironment.WebRootPath;
                var uploads = Path.Combine(wwwRootPath, path);
                var extension = Path.GetExtension(image.FileName);
                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    image.CopyTo(fileStreams);
                }
                obj.Image = $"\\images\\{fileName}" + extension;
            }
            else
            {
                obj.Image = "";
            }

            if (ModelState.IsValid)
            {
                _bookRepository.AddBook(obj);
                TempData["success"] = "Books created successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(_categoryRepo.GetAllCategory(), "Id", "Name", obj.CategoryId);
            return View(obj);
        }

        //GET
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var bookFromDb = _bookRepository.GetBookById(id);
            ViewBag.Categories = new SelectList(_categoryRepo.GetAllCategory(), "Id", "Name");
            if (bookFromDb == null)
            {
                return NotFound();
            }
            return View(bookFromDb);
        }
        //POST
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Book obj)
        {
            var existingImage = obj.Image;
            var image = Request.Form.Files.FirstOrDefault();
            if (image != null)
            {
                var fileName = Guid.NewGuid().ToString();
                var path = $@"images\";
                var wwwRootPath = _webHostEnvironment.WebRootPath;
                var uploads = Path.Combine(wwwRootPath, path);
                var extension = Path.GetExtension(image.FileName);
                var existingFilePath = Path.Combine(uploads, fileName + extension);
                if (System.IO.File.Exists(existingFilePath))
                {
                    fileName = Guid.NewGuid().ToString();
                    existingFilePath = Path.Combine(uploads, fileName + extension);
                }

                using (var fileStreams = new FileStream(existingFilePath, FileMode.Create))
                {
                    image.CopyTo(fileStreams);
                }

                obj.Image = $"\\images\\{fileName}" + extension;
            }
            else
            {
                obj.Image = "";
            }

            if (ModelState.IsValid)
            {
                _bookRepository.UpdateBook(obj);
                TempData["success"] = "Books updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            obj.Image = existingImage;
            ViewBag.Categories = new SelectList(_categoryRepo.GetAllCategory(), "Id", "Name", obj.CategoryId);
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
            var bookFromDb = _bookRepository.GetBookById(id);
            if (bookFromDb == null)
            {
                return NotFound();
            }
            return View(bookFromDb);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int id)
        {
            var bookFromDb = _bookRepository.GetBookById(id);
            if (bookFromDb == null)
            {
                return NotFound();
            }

            _bookRepository.DeleteBook(id);
            TempData["success"] = "Books deleted successfully!";
            return RedirectToAction("Index");

        }

        //GET
        [Authorize]
        public IActionResult Detail(int id)
        {
            BookView bookView = new BookView()
            {
                Books = _bookRepository.GetBookById(id),
                Comments = _commentRepository.GetAllComments(),
				
		};
            return View(bookView);
        }
    }
}


