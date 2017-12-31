using System;
using System.Collections.Generic;
using System.Linq;
using PoolScoreboard.Application.DataAccess.Team;

namespace PoolScoreboard.Application.Interfaces
{
    public interface ITableFactory
    {
        Table Create(Game game, IEnumerable<string> team1, IEnumerable<string> team2);
    }

    public class TableFactory : ITableFactory
    {
        private IPlayerFactory _playerFactory = new PlayerFactory();
        
        public Table Create(Game game, IEnumerable<string> team1, IEnumerable<string> team2)
        {
            switch (game)
            {
                case Game.EightBallPool:
                    return EightBallTable(team1, team2);
                default:
                    throw new NotImplementedException();
            }
        }
        
        private Table EightBallTable(IEnumerable<string> team1, IEnumerable<string> team2)
        {
            var rack = new EightBallPoolRack();
            var firstTeam = new EightBallPoolTeam(team1.Select(p => _playerFactory.Create(p)));
            var secondTeam = new EightBallPoolTeam(team2.Select(p => _playerFactory.Create(p)));
            var table =  new Table(rack, firstTeam, secondTeam);
            SetTeamFrames(firstTeam, secondTeam, table.CurrentFrame);
            return table;
        }

        private void SetTeamFrames(ITeam team1, ITeam team2, Frame frame)
        {
            team1.Frame = frame;
            team2.Frame = frame;
        }
    }
}