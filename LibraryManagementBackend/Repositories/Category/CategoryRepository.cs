namespace LibraryManagementBackend.Repositories.Category
{
    using LibraryManagementBackend.Models;
    using Microsoft.EntityFrameworkCore;

    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(LibraryManagementDbContext context) : base(context)
        {
        }

        protected override DbSet<Category> DbSet => this.context.Categories;
    }
}