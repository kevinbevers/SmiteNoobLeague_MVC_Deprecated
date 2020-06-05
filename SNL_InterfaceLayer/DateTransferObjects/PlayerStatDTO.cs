using System;
using System.Collections.Generic;
using System.Text;

namespace SNL_InterfaceLayer.DateTransferObjects
{
    public class PlayerStatDTO
    {
        public int? PlayerStatID { get; set; }
        public int? PlayerID { get; set; }
        public int? TeamID { get; set; }
        public int? MatchID { get; set; }
        public int? GodPlayedID { get; set; }
        public int PlayerLevel { get; set; }
        public int PlayerKills { get; set; }
        public int PlayerDeaths { get; set; }
        public int PlayerAssists { get; set; }
        public int PlayerGoldPerMinute { get; set; }
        public int PlayerDamage { get; set; }
        public int PlayerDamageTaken { get; set; }
        public int PlayerDamageMitigated { get; set; }
        public int PlayerHealing { get; set; }
        public int PlayerGoldEarned { get; set; }
        public int? PlayerItem1ID { get; set; }
        public int? PlayerItem2ID { get; set; }
        public int? PlayerItem3ID { get; set; }
        public int? PlayerItem4ID { get; set; }
        public int? PlayerItem5ID { get; set; }
        public int? PlayerItem6ID { get; set; }
        public int? PlayerRelic1ID { get; set; }
        public int? PlayerRelic2ID { get; set; }
        public bool PlayerWon { get; set; }
    }
}
