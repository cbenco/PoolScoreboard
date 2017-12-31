using System.Collections.Generic;
using System.Web.Http;
using System.Web.WebSockets;
using PoolScoreboard.Application;
using PoolScoreboard.Application.DataAccess.Shot;
using PoolScoreboard.Application.DataAccess.Team;
using PoolScoreboard.Application.Interfaces;
using PoolScoreboard.Services.Factories;
using PoolScoreboard.Services.Models;

namespace PoolScoreboard.Services.Controllers
{
    public class ScoreboardController : ApiController
    {
        private readonly ITableFactory _tableFactory = new TableFactory();
        private readonly ITableResponseFactory _tableResponseFactory = new TableResponseFactory();

        private readonly IShotResultRepository _shotResultRepository = new ShotResultRepository();
        
        public TableResponse CreateGame(Game gameType, string firstTeamPlayerNames, string secondTeamPlayerNames)
        {
            var table = _tableFactory.Create(gameType, 
                                        GetTeamNameStrings(firstTeamPlayerNames), 
                                        GetTeamNameStrings(secondTeamPlayerNames));


            return _tableResponseFactory.Create(Game.EightBallPool, table);
        }
        
        private IEnumerable<string> GetTeamNameStrings(string playerNames)
        {
            return playerNames.Split(',');
        }
    }
}
