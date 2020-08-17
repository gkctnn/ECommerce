using ECommerce.Core.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ECommerce.Core.Dal
{
    public interface IRepository<T> where T : class, IEntity
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetEx(Expression<Func<T, bool>> predicate);
        T GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        int Save();
    }
}
