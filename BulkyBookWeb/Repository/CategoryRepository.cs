using BulkyBookWeb.Data;
using BulkyBookWeb.Interface;
using BulkyBookWeb.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Web.Mvc;

namespace BulkyBookWeb.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) {
            _db = db;
        }
        public void AddCategory(Category category)
        {
            _db.Database.ExecuteSqlRaw("EXEC createCategory @Name, @DisplayOrder, @CreatedDateTime",
		    new SqlParameter("@Name", category.Name),
		    new SqlParameter("@DisplayOrder", category.DisplayOrder),
		    new SqlParameter("@CreatedDateTime", category.CreatedDateTime));
			_db.SaveChanges();
        }

        public bool DeleteCategory(int id)
        {

             _db.Database.ExecuteSqlRaw("EXEC deleteCategory @CategoryId",
            new SqlParameter("@CategoryId", id));
            _db.SaveChanges();
            return true;
        }

        public IEnumerable<Category> GetAllCategory()
        {
            IEnumerable<Category> objCategoryList = _db.Categories.FromSqlRaw("EXEC getAllCategories");
            return objCategoryList;
        }

        public Category GetCategoryById(int id)
        {
            var categoryById = _db.Categories.FromSqlInterpolated($"EXEC getCategoryById {id}")
			.AsEnumerable();

			return categoryById.SingleOrDefault();
		}

        public void UpdateCategory(Category category)
        {
            _db.Database.ExecuteSqlRaw("EXEC updateCategory @CategoryId, @NewName, @NewDisplayOrder, @NewCreatedDateTime",
		new SqlParameter("@CategoryId", category.Id),
		new SqlParameter("@NewName", category.Name),
		new SqlParameter("@NewDisplayOrder", category.DisplayOrder),
		new SqlParameter("@NewCreatedDateTime", category.CreatedDateTime));
			_db.SaveChanges();
        }
    }
}
