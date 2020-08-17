using ECommerce.Business.Abstract.Order;
using ECommerce.Entities.Order;
using ECommerce.DataAccess.Abstract.Order;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace ECommerce.Business.Order
{
    public class ShoppingCartItemManager : IShoppingCartItemService
    {
        IShoppingCartItemRepository _shoppingCartItemRepository;
        public ShoppingCartItemManager(IShoppingCartItemRepository shoppingCartItemRepository)
        {
            _shoppingCartItemRepository = shoppingCartItemRepository;
        }

        public void Delete(ShoppingCartItem entity)
        {
            _shoppingCartItemRepository.Delete(entity);
        }

        public List<ShoppingCartItem> GetAll()
        {
            return _shoppingCartItemRepository.GetAll().ToList();
        }

        public ShoppingCartItem GetById(int id)
        {
            return _shoppingCartItemRepository.GetById(id);
        }

        public List<ShoppingCartItem> GetEx(Expression<Func<ShoppingCartItem, bool>> predicate)
        {
            return _shoppingCartItemRepository.GetEx(predicate).ToList();
        }

        public void Insert(ShoppingCartItem entity)
        {
            _shoppingCartItemRepository.Insert(entity);
        }

        public void Update(ShoppingCartItem entity)
        {
            _shoppingCartItemRepository.Update(entity);
        }
    }
}
