using System;
using System.Collections.Generic;
using System.Text;
using SNL_PersistenceLayer.Interfaces;
using SNL_InterfaceLayer.DateTransferObjects;
using SNL_InterfaceLayer.CustomExceptions;

namespace SNL_PersistenceLayer.Contexts
{
    public class PlayerContext : IContext<PlayerDTO>
    {
        public void Add(PlayerDTO entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PlayerDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public PlayerDTO GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(PlayerDTO entity)
        {
            throw new NotImplementedException();
        }

        public void Update(PlayerDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
