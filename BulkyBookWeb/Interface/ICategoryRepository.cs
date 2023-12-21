using BulkyBookWeb.Models;

namespace BulkyBookWeb.Interface
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategory();
        Category GetCategoryById(int id);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        bool DeleteCategory(int id);
    }
}
