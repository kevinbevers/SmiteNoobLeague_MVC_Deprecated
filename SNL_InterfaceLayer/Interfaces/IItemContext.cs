using System;
using System.Collections.Generic;
using System.Text;
using SNL_InterfaceLayer.DateTransferObjects;

namespace SNL_InterfaceLayer.Interfaces
{
    public interface IItemContext : IContext<ItemDTO>
    {
        int? AddMultiple(IEnumerable<ItemDTO> entityList);
    }
}
