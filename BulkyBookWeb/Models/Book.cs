using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyBookWeb.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string? Image { get; set; }
        public DateTime PublishedDate { get; set; }
        [ForeignKey("Categories")]
        public int CategoryId { get; set; }
        public Category? Categories { get; set; }
        public IEnumerable<Comment>? Comments { get; set; }
        public bool? IsDeleted { get; set; }=false;
    }
}
