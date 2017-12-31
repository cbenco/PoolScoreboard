using System;
using System.Collections.Generic;
using System.Web.Http;
using PoolScoreboard.Application;
using PoolScoreboard.Application.DataAccess.Match;
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
        
        private readonly IPoolTeamRepository _teamRepository = new PoolTeamRepository();
        
        private readonly FrameRepository _frameRepository = new FrameRepository();
        
        public TableResponse CreateGame(Game gameType, string firstTeamPlayerNames, string secondTeamPlayerNames)
        {
            var table = _tableFactory.Create(gameType, 
                                        GetTeamNameStrings(firstTeamPlayerNames), 
                                        GetTeamNameStrings(secondTeamPlayerNames));

            _teamRepository.Save((EightBallPoolTeam)table.Team1);
            _teamRepository.Save((EightBallPoolTeam)table.Team2);
            
            return _tableResponseFactory.Create(Game.EightBallPool, table);
        }
        
        private IEnumerable<string> GetTeamNameStrings(string playerNames)
        {
            return playerNames.Split(',');
        }
    }
}