using ECommerce.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ECommerce.Business.Abstract.Order
{
    public interface IShoppingCartItemService
    {
        List<ShoppingCartItem> GetAll();
        List<ShoppingCartItem> GetEx(Expression<Func<ShoppingCartItem, bool>> predicate);
        ShoppingCartItem GetById(int id);
        void AddToCart(string customerUsername, ShoppingCartItem entity);
        void RemoveFromCart(ShoppingCartItem entity);
        void ClearCart(List<ShoppingCartItem> entities);
    }
}
