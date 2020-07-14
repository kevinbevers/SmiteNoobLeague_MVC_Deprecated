﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SNL_InterfaceLayer.API_Models
{
    public class ApiPlayerMatchStat
    {
        public int Account_Level { get; set; }
        public int ActiveId1 { get; set; }
        public int ActiveId2 { get; set; }
        public int ActiveId3 { get; set; }
        public int ActiveId4 { get; set; }
        public int ActivePlayerId { get; set; }
        public int Assists { get; set; }
        public string Ban1 { get; set; }
        public string Ban10 { get; set; }
        public int Ban10Id { get; set; }
        public int Ban1Id { get; set; }
        public string Ban2 { get; set; }
        public int Ban2Id { get; set; }
        public string Ban3 { get; set; }
        public int Ban3Id { get; set; }
        public string Ban4 { get; set; }
        public int Ban4Id { get; set; }
        public string Ban5 { get; set; }
        public int Ban5Id { get; set; }
        public string Ban6 { get; set; }
        public int Ban6Id { get; set; }
        public string Ban7 { get; set; }
        public int Ban7Id { get; set; }
        public string Ban8 { get; set; }
        public int Ban8Id { get; set; }
        public string Ban9 { get; set; }
        public int Ban9Id { get; set; }
        public int Camps_Cleared { get; set; }
        public int Conquest_Losses { get; set; }
        public int Conquest_Points { get; set; }
        public int Conquest_Tier { get; set; }
        public int Conquest_Wins { get; set; }
        public int Damage_Bot { get; set; }
        public int Damage_Done_In_Hand { get; set; }
        public int Damage_Done_Magical { get; set; }
        public int Damage_Done_Physical { get; set; }
        public int Damage_Mitigated { get; set; }
        public int Damage_Player { get; set; }
        public int Damage_Taken { get; set; }
        public int Damage_Taken_Magical { get; set; }
        public int Damage_Taken_Physical { get; set; }
        public int Deaths { get; set; }
        public int Distance_Traveled { get; set; }
        public int Duel_Losses { get; set; }
        public int Duel_Points { get; set; }
        public int Duel_Tier { get; set; }
        public int Duel_Wins { get; set; }
        public DateTime Entry_Datetime { get; set; }
        public int Final_Match_Level { get; set; }
        public string First_Ban_Side { get; set; }
        public int GodId { get; set; }
        public int Gold_Earned { get; set; }
        public int Gold_Per_Minute { get; set; }
        public int Healing { get; set; }
        public int Healing_Bot { get; set; }
        public int Healing_Player_Self { get; set; }
        public int ItemId1 { get; set; }
        public int ItemId2 { get; set; }
        public int ItemId3 { get; set; }
        public int ItemId4 { get; set; }
        public int ItemId5 { get; set; }
        public int ItemId6 { get; set; }
        public string Item_Active_1 { get; set; }
        public string Item_Active_2 { get; set; }
        public string Item_Active_3 { get; set; }
        public string Item_Active_4 { get; set; }
        public string Item_Purch_1 { get; set; }
        public string Item_Purch_2 { get; set; }
        public string Item_Purch_3 { get; set; }
        public string Item_Purch_4 { get; set; }
        public string Item_Purch_5 { get; set; }
        public string Item_Purch_6 { get; set; }
        public int Joust_Losses { get; set; }
        public int Joust_Points { get; set; }
        public int Joust_Tier { get; set; }
        public int Joust_Wins { get; set; }
        public int Killing_Spree { get; set; }
        public int Kills_Bot { get; set; }
        public int Kills_Double { get; set; }
        public int Kills_Fire_Giant { get; set; }
        public int Kills_First_Blood { get; set; }
        public int Kills_Gold_Fury { get; set; }
        public int Kills_Penta { get; set; }
        public int Kills_Phoenix { get; set; }
        public int Kills_Player { get; set; }
        public int Kills_Quadra { get; set; }
        public int Kills_Siege_Juggernaut { get; set; }
        public int Kills_Single { get; set; }
        public int Kills_Triple { get; set; }
        public int Kills_Wild_Juggernaut { get; set; }
        public string Map_Game { get; set; }
        public int Mastery_Level { get; set; }
        public int Match { get; set; }
        public int Match_Duration { get; set; }
        public object MergedPlayers { get; set; }
        public int Minutes { get; set; }
        public int Multi_kill_Max { get; set; }
        public int Objective_Assists { get; set; }
        public int PartyId { get; set; }
        public double Rank_Stat_Conquest { get; set; }
        public double Rank_Stat_Duel { get; set; }
        public double Rank_Stat_Joust { get; set; }
        public string Reference_Name { get; set; }
        public string Region { get; set; }
        public string Skin { get; set; }
        public int SkinId { get; set; }
        public int Structure_Damage { get; set; }
        public int Surrendered { get; set; }
        public int TaskForce { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public int TeamId { get; set; }
        public string Team_Name { get; set; }
        public int Time_In_Match_Seconds { get; set; }
        public int Towers_Destroyed { get; set; }
        public int Wards_Placed { get; set; }
        public string Win_Status { get; set; }
        public int Winning_TaskForce { get; set; }
        public string hasReplay { get; set; }
        public object hz_gamer_tag { get; set; }
        public string hz_player_name { get; set; }
        public int match_queue_id { get; set; }
        public string name { get; set; }
        public string playerId { get; set; }
        public string playerName { get; set; }
        public string playerPortalId { get; set; }
        public string playerPortalUserId { get; set; }
        public object ret_msg { get; set; }
    }
}
