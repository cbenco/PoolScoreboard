using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PoolScoreboard.Application;
using PoolScoreboard.Application.Interfaces;

namespace PoolScoreboard.Services.Controllers
{
    public class ScoreboardController : ApiController
    {
        private ITableFactory _tableFactory = new TableFactory();
        public string Get()
        {
            return "Hello World";
        }

        public Table CreateGame(Game gameType, string firstTeamPlayerNames, string secondTeamPlayerNames)
        {
            return _tableFactory.Create(gameType, firstTeamPlayerNames.Split(','), secondTeamPlayerNames.Split(','));
        }
    }
}
