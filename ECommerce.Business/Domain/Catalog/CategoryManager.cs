using ECommerce.Business.Abstract.Domain.Catalog;
using ECommerce.Entities.Domain.Catalog;
using ECommerce.DataAccess.Abstract.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace ECommerce.Business.Domain.Catalog
{
    public class CategoryManager : ICategoryService
    {
        ICategoryRepository _categoryRepository;
        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void Delete(Category entity)
        {
            _categoryRepository.Delete(entity);
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll().ToList();
        }

        public Category GetById(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public List<Category> GetEx(Expression<Func<Category, bool>> predicate)
        {
            return _categoryRepository.GetEx(predicate).ToList();
        }

        public void Insert(Category entity)
        {
            _categoryRepository.Insert(entity);
        }

        public void Update(Category entity)
        {
            _categoryRepository.Update(entity);
        }
    }
}
