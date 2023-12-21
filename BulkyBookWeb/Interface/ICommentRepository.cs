using BulkyBookWeb.Models;

namespace BulkyBookWeb.Interface
{
	public interface ICommentRepository
	{
		IEnumerable<Comment> GetAllComments();
		Comment GetCommentById(int id);
		void AddComment(Comment comment);
		void UpdateComment(Comment comment);
		IEnumerable<Comment> GetAllCommentsByBook(int BookId);

	}
}
