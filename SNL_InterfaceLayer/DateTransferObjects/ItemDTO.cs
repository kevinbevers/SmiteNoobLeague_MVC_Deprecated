using System;
using System.Collections.Generic;
using System.Text;

namespace SNL_InterfaceLayer.DateTransferObjects
{
    public class ItemDTO
    {
        public int? ItemID { get; set; }
        public string ItemName { get; set; }
        public byte[] ItemIcon { get; set; }
        public string ItemDescription {get; set;}
        public int ItemPrice { get; set; }
        public string ItemShortDescription { get; set; }
        public IEnumerable<string> ItemStats { get; set; }
    }
}
