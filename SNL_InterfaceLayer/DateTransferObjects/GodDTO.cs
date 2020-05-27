using System;
using System.Collections.Generic;
using System.Text;

namespace SNL_InterfaceLayer.DateTransferObjects
{
    public class GodDTO
    {
        public int GodID { get; set; }
        public string GodName { get; set; }
        public byte[] GodIcon { get; set; }
        public byte[] GodCardArt { get; set; }
        public string GodClass { get; set; }
        public string GodTitle { get; set; }
        public string GodLore { get; set; }
    }
}
