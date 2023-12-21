using Microsoft.Data.SqlClient.DataClassification;

namespace BulkyBookWeb.Models
{
	public class Comment
	{
		public int Id { get; set; }
		public string? Content { get; set; }
		public int BookId { get; set; }
	}
}
