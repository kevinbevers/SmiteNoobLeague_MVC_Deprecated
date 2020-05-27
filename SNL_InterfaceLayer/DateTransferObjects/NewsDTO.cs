using System;
using System.Collections.Generic;
using System.Text;

namespace SNL_InterfaceLayer.DateTransferObjects
{
    public class NewsDTO
    {
        public int NewsID { get; set; }
        public string NewsTitle { get; set; }
        public string NewsSubject { get; set; }
        public DateTime NewsDate { get; set; }
        public string NewsArticle { get; set; }
    }
}
