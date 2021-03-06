﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SNL_LogicLayer.Models
{
    public class Player
    {
        public int? PlayerID { get; set; }
        public string PlayerName { get; set; }
        public int? PlayerPlatformID { get; set; }
        public string PlayerPlatformName { get; set; }
        public Role PlayerRole { get; set; }
    }
}
