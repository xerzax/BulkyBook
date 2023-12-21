using BulkyBookWeb.Data;

namespace BulkyBookWeb.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int Stars { get; set; }
        public int BookId {  get; set; }

		
	}
}
