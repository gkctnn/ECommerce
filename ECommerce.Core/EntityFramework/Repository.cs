using ECommerce.Core.Dal;
using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ECommerce.Core.EntityFramework
{
    public class Repository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext
    {
        protected readonly TContext _context;
        public Repository(TContext context)
        {
            _context = context;
        }

        public void Delete(TEntity entity)
        {
            entity.Deleted = true;
            Update(entity);
        }

        public void DeleteAll(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.Deleted = true;
                Update(entity);
            }            
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> GetEx(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public void Insert(TEntity entity)
        {
            entity.CreatedOnUtc = DateTime.Now;
            entity.UpdatedOnUtc = DateTime.Now;
            _context.Set<TEntity>().Add(entity);
            Save();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            entity.UpdatedOnUtc = DateTime.Now;
            _context.Set<TEntity>().Update(entity);
            Save();
        }
    }
}
