using BulkyBookWeb.Interface;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class RatingController : Controller
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IBookRepository _bookRepository;

        public RatingController(IRatingRepository ratingRepository, IBookRepository bookRepository)
        {
            _ratingRepository = ratingRepository;
            _bookRepository = bookRepository;
        }
        [HttpPost]
        public IActionResult SetRating(BookView obj)
        {
            if (ModelState.IsValid)
            {
                var rating = new Rating
                {
                    BookId = obj.Rating.BookId,
                    Stars = obj.Rating.Stars,
                };

                _ratingRepository.SetRating(rating);
                TempData["success"] = "Rating set successfully!";
                return RedirectToAction("Detail", "Book", new { id = rating.BookId });
            }

            return RedirectToAction("Detail", "Book");

        }
    }
}