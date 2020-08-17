using ECommerce.Core.EntityFramework;
using ECommerce.DataAccess.Abstract.Domain.Catalog;
using ECommerce.Entities.Domain.Catalog;

namespace ECommerce.DataAccess.EntityFramework.Domain.Catalog
{
    public class CategoryRepository : Repository<Category, AppDbContext>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
