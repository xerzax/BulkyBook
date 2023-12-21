using BulkyBookWeb.Data;
using BulkyBookWeb.Interface;
using BulkyBookWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Repository
{
	public class CommentRepository : ICommentRepository
	{
		private readonly ApplicationDbContext _db;
		public CommentRepository(ApplicationDbContext db)
		{
			_db = db;
		}
		public void AddComment(Comment comment)
		{
			_db.Comments.Add(comment);
			_db.SaveChanges();
		}

		public void DeleteComment(int id)
		{
			var obj = _db.Comments.Find(id);
			_db.Comments.Remove(obj);
			_db.SaveChanges();
			
		}

		public IEnumerable<Comment> GetAllComments()
		{
			IEnumerable<Comment> objCommentList = _db.Comments;
			return objCommentList;
		}

		public Comment GetCommentById(int id)
		{
			var commentById = _db.Comments.Find(id);
			return commentById;
		}

		public void UpdateComment(Comment comment)
		{
			_db.Comments.Update(comment);
			_db.SaveChanges();
		
		}
		
		public IEnumerable<Comment> GetAllCommentsByBook(int BookId)
		{
            var comments = _db.Comments.Where(x => x.BookId == BookId);
			return comments;
        }
	}
}
