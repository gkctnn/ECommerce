using ECommerce.Business.Abstract.Order;
using ECommerce.Entities.Order;
using ECommerce.DataAccess.Abstract.Order;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using ECommerce.DataAccess.Abstract.Domain.Catalog;

namespace ECommerce.Business.Order
{
    public class ShoppingCartItemManager : IShoppingCartItemService
    {
        IShoppingCartItemRepository _shoppingCartItemRepository;
        IProductRepository _productRepository;
        public ShoppingCartItemManager(IShoppingCartItemRepository shoppingCartItemRepository, IProductRepository productRepository)
        {
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _productRepository = productRepository;
        }

        public void ClearCart(List<ShoppingCartItem> entities)
        {
            _shoppingCartItemRepository.DeleteAll(entities);
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

        public void AddToCart(string customerUsername, ShoppingCartItem entity)
        {
            var product = _productRepository.GetById(entity.ProductId);

            product.StockQuantity -= entity.Quantity;
            _productRepository.Update(product);

            var shoppingCartItem = _shoppingCartItemRepository.GetEx(s => s.CustomerUserName == customerUsername && s.ProductId == entity.ProductId).FirstOrDefault();

            if (shoppingCartItem == null)
            {
                _shoppingCartItemRepository.Insert(entity);
            }
            else
            {
                shoppingCartItem.Quantity += entity.Quantity;
                _shoppingCartItemRepository.Update(entity);
            }
        }

        public void RemoveFromCart(ShoppingCartItem entity)
        {
            if (entity.Quantity > 1)
            {
                entity.Quantity--;

                _shoppingCartItemRepository.Update(entity);
            }
            else
            {
                _shoppingCartItemRepository.Delete(entity);
            }
        }
    }
}
