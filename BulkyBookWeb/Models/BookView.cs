using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class BookView
    {
        //public Book Books { get; set; }
        //public List<Comment> Comments { get; set; }
        public Book? Books { get; set; }
        public Comment? Comment { get; set; } // This is for addition of a new comment.
        public IEnumerable<Comment>? Comments { get; set; } // This is for displaying all the list of available comments of the movie.

        public Rating? Rating { get; set; }
    }
}
