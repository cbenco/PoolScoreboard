using System.Collections.Generic;
using System.Linq;
using PoolScoreboard.Application;

namespace PoolScoreboard.Interface
{
    public class GameController
    {
        public Table CreateEightBallPoolGame(ITeam team1, ITeam team2)
        {
            IRack rack = new EightBallPoolRack();
            return new Table(rack, team1, team2);
        }
        
        public ITeam CreateTeam(IEnumerable<string> playerNames)
        {
            return new EightBallPoolTeam(playerNames.Select(name => new Player
            {
                Id = 0,
                Name = name
            }))
            {
                Shooting = BallClass.Neither
            };
        }

        public void TakeShot()
        {
            
        }
    }
}