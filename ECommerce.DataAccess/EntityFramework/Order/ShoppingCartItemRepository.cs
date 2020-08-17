using ECommerce.Core.EntityFramework;
using ECommerce.DataAccess.Abstract.Order;
using ECommerce.Entities.Order;

namespace ECommerce.DataAccess.EntityFramework.Order
{
    public class ShoppingCartItemRepository : Repository<ShoppingCartItem, AppDbContext>, IShoppingCartItemRepository
    {
        public ShoppingCartItemRepository(AppDbContext context) : base(context)
        {
        }
    }
}
