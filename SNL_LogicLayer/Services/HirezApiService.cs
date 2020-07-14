using SNL_InterfaceLayer.API_Models;
using SNL_InterfaceLayer.Interfaces;
using SNL_LogicLayer.Models;
using SNL_LogicLayer.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace SNL_LogicLayer.Services
{
    public class HirezApiService : IHirezApiService
    {
        private readonly IHirezApiContext _hirezApi;
        public HirezApiService(IHirezApiContext hirezApi)
        {
            _hirezApi = hirezApi;
        }
        public async Task<string> GetCurrentPatchInfoAsync()
        {
            ApiPatchInfo patchInfo = await _hirezApi.GetPatchInfo();

            return patchInfo.version_string;
        }

        public async Task<Match> GetMatchDetails(int MatchID)
        {
            List<ApiPlayerMatchStat> matchDetails = (List<ApiPlayerMatchStat>)await _hirezApi.GetMatchDetailsByMatchID(MatchID);
           
            Match m = new Match();
            m.ApiMatchID = MatchID;
            m.MatchDate = matchDetails[0].Entry_Datetime;
            m.PatchNumber = "patch number here";
            m.GodBanList = new List<int> {
                matchDetails[0].Ban1Id, 
                matchDetails[0].Ban2Id, 
                matchDetails[0].Ban3Id, 
                matchDetails[0].Ban4Id,
                matchDetails[0].Ban5Id, 
                matchDetails[0].Ban6Id,
                matchDetails[0].Ban7Id,
                matchDetails[0].Ban8Id,
                matchDetails[0].Ban9Id,
                matchDetails[0].Ban10Id,
            };
            List<PlayerStat> playerStats = new List<PlayerStat>();
            foreach(var player in matchDetails)
            {
                playerStats.Add(new PlayerStat { 
                    PlayerID = player.playerId, 
                    GodPlayedID = player.GodId, 
                    PlayerWon = Convert.ToBoolean(player.Win_Status),
                    PlayerLevel = player.Final_Match_Level,
                    PlayerKills = player.Kills_Player,
                    PlayerDeaths = player.Deaths,
                    PlayerAssists = player.Assists,
                    PlayerDamage = player.Damage_Player,
                    PlayerDamageMitigated = player.Damage_Mitigated,
                    PlayerDamageTaken = player.Damage_Taken,
                    PlayerHealing = player.Healing,
                    PlayerGoldEarned = player.Gold_Earned,
                    PlayerGoldPerMinute = player.Gold_Per_Minute,
                    PlayerItem1ID = player.ItemId1,
                    PlayerItem2ID = player.ItemId2,
                    PlayerItem3ID = player.ItemId3,
                    PlayerItem4ID = player.ItemId4,
                    PlayerItem5ID = player.ItemId5,
                    PlayerItem6ID = player.ItemId6,
                    PlayerRelic1ID = player.ActiveId1,
                    PlayerRelic2ID = player.ActiveId2,
                });
            }
            m.playerStats = playerStats;

            return m;
        }

        public async Task<IEnumerable<Player>> SearchPlayersByName(string name)
        {
            var playersFound = await _hirezApi.SearchPlayerByName(name);
            List<Player> p = new List<Player>();
            foreach(var player in playersFound)
            {
                p.Add(new Player { PlayerName = player.Name, PlayerID = player.player_id, PlayerPlatformID = player.portal_id });
            }
            return p;
        }

        public Task<int?> UpdateGods()
        {
            throw new NotImplementedException();
        }

        public Task<int?> UpdateItems()
        {
            throw new NotImplementedException();
        }
    }
}
