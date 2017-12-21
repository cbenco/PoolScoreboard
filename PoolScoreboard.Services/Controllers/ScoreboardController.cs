using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PoolScoreboard.Application;
using PoolScoreboard.Application.Interfaces;
using PoolScoreboard.Services.Models;

namespace PoolScoreboard.Services.Controllers
{
    public class ScoreboardController : ApiController
    {
        private ITableFactory _tableFactory = new TableFactory();
        public string Get()
        {
            return "Hello World";
        }

        public TableResponse CreateGame(Game gameType, string firstTeamPlayerNames, string secondTeamPlayerNames)
        {
            var table = _tableFactory.Create(gameType, 
                                        GetTeamNameStrings(firstTeamPlayerNames), 
                                        GetTeamNameStrings(secondTeamPlayerNames));

            return new TableResponse(table);
        }
        
        private IEnumerable<string> GetTeamNameStrings(string playerNames)
        {
            return playerNames.Split(',');
        }
    }
}
