using SNL_LogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SNL_LogicLayer.ServiceInterfaces
{
    public interface IHirezApiService
    {
        Task<IEnumerable<Player>> SearchPlayersByName(string name);
        Task<Match> GetMatchDetails(int MatchID);
        Task<string> GetCurrentPatchInfoAsync();
        Task<int?> UpdateGods();
        Task<int?> UpdateItems();
    }
}
