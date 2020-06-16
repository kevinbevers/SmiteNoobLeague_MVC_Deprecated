using System;
using System.Collections.Generic;
using System.Text;
using SNL_InterfaceLayer.DateTransferObjects;

namespace SNL_InterfaceLayer.Interfaces
{
    public interface IPlayerContext
    {
        void Add(PlayerDTO entity);
        void AddMultiple(IEnumerable<PlayerDTO> entity);
        PlayerDTO GetByID(int? id);
        IEnumerable<PlayerDTO> GetAll();
        void Update(PlayerDTO entity);
        void Remove(PlayerDTO entity);
    }
}
