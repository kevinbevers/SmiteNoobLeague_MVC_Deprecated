using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SNL_PersistenceLayer.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetByID(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void Remove(TEntity entity);
    }
}
