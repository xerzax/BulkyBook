using BulkyBookWeb.Data;
using BulkyBookWeb.Interface;
using BulkyBookWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _db;
        public BookRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void AddBook(Book book)
        {
            _db.Books.Add(book);
            _db.SaveChanges();
        }

        public void DeleteBook(int id)
        {
            var obj = _db.Books.Find(id);
            _db.Books.Update(obj);
            obj.IsDeleted = true;
            _db.SaveChanges();
        }

        public IEnumerable<Book> GetAllBooks()
        {
            var objBookList = _db.Books.Include(b => b.Categories).ToList();
            return objBookList;
        }

        public Book GetBookById(int id)
        {
            var bookById = _db.Books.Include(b => b.Categories).FirstOrDefault(b => b.Id == id);
            return bookById;
        }

        public void UpdateBook(Book book)
        {
			_db.Books.Update(book);
            book.Image = book.Image;
            _db.SaveChanges();
        }

    }

}
