using ECommerce.Core.EntityFramework;
using ECommerce.DataAccess.Abstract.Domain.Catalog;
using ECommerce.Entities.Domain.Catalog;

namespace ECommerce.DataAccess.EntityFramework.Domain.Catalog
{
    public class ProductRepository : Repository<Product, AppDbContext>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
