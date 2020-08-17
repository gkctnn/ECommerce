using ECommerce.Entities.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ECommerce.Business.Abstract.Domain.Catalog
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        List<Category> GetEx(Expression<Func<Category, bool>> predicate);
        Category GetById(int id);
        void Insert(Category entity);
        void Update(Category entity);
        void Delete(Category entity);
    }
}
