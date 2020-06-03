using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SNL_PersistenceLayer.Interfaces
{
    public interface IContext<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        TEntity GetByID(int? id);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
