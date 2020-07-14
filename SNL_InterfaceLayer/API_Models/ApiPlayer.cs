using System;
using System.Collections.Generic;
using System.Text;

namespace SNL_InterfaceLayer.API_Models
{
    public class ApiPlayer
    {
        public string Name { get; set; }
        public int player_id { get; set; }
        public int portal_id { get; set; }
        public object ret_msg { get; set; }
    }
}
