using ECommerce.Entities.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ECommerce.Business.Abstract.Domain.Catalog
{
    public interface IProductService
    {
        List<Product> GetAll();
        List<Product> GetEx(Expression<Func<Product, bool>> predicate);
        Product GetById(int id);
        void Insert(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
    }
}
