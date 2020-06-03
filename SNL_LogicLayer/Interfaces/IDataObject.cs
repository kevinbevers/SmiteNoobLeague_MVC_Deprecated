﻿using System;
using System.Collections.Generic;
using System.Text;
using SNL_PersistenceLayer.Interfaces;

namespace SNL_LogicLayer.Interfaces
{ 
    interface IDataObject<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        TEntity GetByID(int id);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
