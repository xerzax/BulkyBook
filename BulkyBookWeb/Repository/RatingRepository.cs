using BulkyBookWeb.Data;
using BulkyBookWeb.Interface;
using BulkyBookWeb.Models;

namespace BulkyBookWeb.Repository
{
    public class RatingRepository: IRatingRepository
    {
        private readonly ApplicationDbContext _db;
        public RatingRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void SetRating(Rating rating)
        {
            _db.Ratings.Add(rating);
            _db.SaveChanges();

        }
    }
}
