﻿using System;
using System.Collections.Generic;
using System.Text;
using SNL_InterfaceLayer.DateTransferObjects;

namespace SNL_InterfaceLayer.Interfaces
{
    public interface IScheduleContext : IContext<ScheduleDTO>
    {
        IEnumerable<ScheduleDTO> GetByDivisionID(int? id);
    }
}
