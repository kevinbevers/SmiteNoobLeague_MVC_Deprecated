using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SNL_PersistenceLayer.Interfaces
{
    public interface IContext<TEntity> where TEntity : class
    {
        TEntity GetByID(int id);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
    }
}
