using SNL_InterfaceLayer.API_Models;
using SNL_InterfaceLayer.Interfaces;
using SNL_LogicLayer.Models;
using SNL_LogicLayer.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using SNL_InterfaceLayer.DateTransferObjects;
using System.Net;

namespace SNL_LogicLayer.Services
{
    public class HirezApiService : IHirezApiService
    {
        private readonly IHirezApiContext _hirezApi;
        private readonly IGodContext _godContext;
        private readonly IItemContext _itemContext;

        public HirezApiService(IHirezApiContext hirezApi, IGodContext godContext, IItemContext itemContext)
        {
            _hirezApi = hirezApi;
            _godContext = godContext;
            _itemContext = itemContext;
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
                p.Add(new Player { PlayerName = player.Name, PlayerID = player.player_id, PlayerPlatformID = (int)player.portal_id, PlayerPlatformName = player.portal_id.ToString()});
            }
            return p;
        }

        public async Task<int?> UpdateGods()
        {
            IEnumerable<ApiGod> apiGods = await _hirezApi.GetAllGods();
            int countNewAdditions = 0;

            foreach(ApiGod god in apiGods)
            {
                var GodInDB = _godContext.GetByID((int)god.Id);
                GodDTO gDTO = new GodDTO
                {
                    GodID = (int?)god.Id,
                    GodName = god.Name,
                    GodTitle = god.Title,
                    GodClass = god.Classes.ToString(),
                    GodLore = god.Lore,
                };
                using (var webClient = new WebClient())
                {
                    try
                    {
                        byte[] icon = webClient.DownloadData(god.GodIcon_URL);
                        byte[] CardArt = webClient.DownloadData(god.GodCard_Url);

                        gDTO.GodIcon = icon;
                        gDTO.GodCardArt = CardArt;
                    }
                    catch
                    {
                        //"Couldn't update god because the api returned a bad gateway, image / data is likely not implemented yet";
                    }
                }

                if (GodInDB.GodName == null)
                {
                    //create               
                    _godContext.Add(gDTO);
                    countNewAdditions++;
                }
                else
                {
                    //update
                    _godContext.Update(gDTO);
                }
            }

            return countNewAdditions;
        }

        public async Task<int?> UpdateItems()
        {
            IEnumerable<ApiItem> apiItems = await _hirezApi.GetAllItems();
            int countNewAdditions = 0;

            foreach (ApiItem item in apiItems)
            {
                var ItemInDB = _itemContext.GetByID((int)item.ItemId);
                ItemDTO iDTO = new ItemDTO
                {
                    ItemID = (int?)item.ItemId,
                    ItemName = item.DeviceName,
                    ItemDescription = item.ItemDescription.SecondaryDescription != null ? item.ItemDescription.SecondaryDescription.ToString() : item.ItemDescription.Description,
                    ItemShortDescription = item.ShortDesc,
                    ItemPrice = item.Price,
                };
                //Add the itemStats ranging from 1 to 4 stats
                List<string> Stats = new List<string>();
                int statCount = item.ItemDescription.Menuitems.Count();
                //variable to set = the if statement ? if true do this : else do this
                Stats.Add(statCount > 0 ? item.ItemDescription.Menuitems[0].Value + " " + item.ItemDescription.Menuitems[0].Description : null);
                Stats.Add(statCount > 1 ? item.ItemDescription.Menuitems[1].Value + " " + item.ItemDescription.Menuitems[1].Description : null);
                Stats.Add(statCount > 2 ? item.ItemDescription.Menuitems[2].Value + " " + item.ItemDescription.Menuitems[2].Description : null);
                Stats.Add(statCount > 3 ? item.ItemDescription.Menuitems[3].Value + " " + item.ItemDescription.Menuitems[3].Description : null);
                iDTO.ItemStats = Stats;

                using (var webClient = new WebClient())
                {
                    try
                    {
                        byte[] icon = webClient.DownloadData(item.ItemIcon_Url);

                        iDTO.ItemIcon = icon;
                    }
                    catch
                    {
                        //something went wrong trying to get the image....
                    }
                }

                if (ItemInDB == null)
                {
                    //create               
                    _itemContext.Add(iDTO);
                    countNewAdditions++;
                }
                else
                {
                    //update
                    _itemContext.Update(iDTO);
                }
            }
            //return number of added items.
            return countNewAdditions;
        }
    }
}
