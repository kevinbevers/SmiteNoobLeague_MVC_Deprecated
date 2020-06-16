using System;
using System.Collections.Generic;
using System.Text;

namespace SNL_LogicLayer.ServiceInterfaces
{
    public interface IService<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        TEntity GetByID(int id);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
