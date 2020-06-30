using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SNL_InterfaceLayer.Interfaces
{
    public interface IContext<TEntity> where TEntity : class
    {
        int? Add(TEntity entity);
        TEntity GetByID(int? id);
        IEnumerable<TEntity> GetAll();
        int? Update(TEntity entity);
        int? Remove(TEntity entity);
    }
}
