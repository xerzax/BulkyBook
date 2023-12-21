using BulkyBookWeb.Interface;
using BulkyBookWeb.Models;
using BulkyBookWeb.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IBookRepository _bookRepository;

        public CommentController(ICommentRepository commentRepository, IBookRepository bookRepository)
        {
            _commentRepository = commentRepository;
            _bookRepository = bookRepository;
        }
        public IActionResult Index()
        {
            var returnedComments = _commentRepository.GetAllComments();
            return View(returnedComments);
        }

       
        [HttpPost]
        public IActionResult CreateComment(BookView obj)
        {
            if (ModelState.IsValid)
            {
                var comment = new Comment()
                {
                    Content = obj.Comment.Content,
                    BookId = obj.Comment.BookId
                };

                _commentRepository.AddComment(comment);
                TempData["success"] = "Comment posted successfully!";
                int x = comment.BookId;
                var book = _bookRepository.GetBookById(x);
                return RedirectToAction("Detail", "Book", new { id = x });

            }
            return RedirectToAction("Detail", "Book");
        }
        //GET
        public IActionResult Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var commentFromDb = _commentRepository.GetCommentById(id);
            if (commentFromDb == null)
            {
                return NotFound();
            }
            return View(commentFromDb);

        }
        //POST
        [HttpPost]
        public IActionResult Edit(Comment obj) 
        {
            if (ModelState.IsValid)
            {
                _commentRepository.UpdateComment(obj);
                TempData["success"] = "Comment updated successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


    }
}
