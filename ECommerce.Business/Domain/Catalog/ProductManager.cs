using ECommerce.Business.Abstract.Domain.Catalog;
using ECommerce.Entities.Domain.Catalog;
using ECommerce.DataAccess.Abstract.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace ECommerce.Business.Domain.Catalog
{
    public class ProductManager : IProductService
    {
        IProductRepository _productRepository;
        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void Delete(Product entity)
        {
            _productRepository.Delete(entity);
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll().ToList();
        }

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public List<Product> GetEx(Expression<Func<Product, bool>> predicate)
        {
            return _productRepository.GetEx(predicate).ToList();
        }

        public void Insert(Product entity)
        {
            _productRepository.Insert(entity);
        }

        public void Update(Product entity)
        {
            _productRepository.Update(entity);
        }
    }
}
