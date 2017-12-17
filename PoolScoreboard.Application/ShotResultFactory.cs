using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;

namespace PoolScoreboard.Application
{
    public interface IShotResultFactory
    {
        
    }
    
    public class ShotResultFactory : IShotResultFactory
    {
        public ShotResult Create(Game game, ITeam team, IRack rack, 
            string objectBall, IEnumerable<string> sunk)
        {
            sunk = sunk.ToList();
            var isBreak = rack.IsBreak;
            
            ShotResult result = null;
            if (game == Game.EightBallPool)
            {
                result = new PoolShotResult();
                result.ShootingTeam = team;
                result.Shooter = team.ThisShooter;
                result.ObjectBall = int.TryParse(objectBall, out var i)
                    ? rack.Ball(objectBall)
                    : null;
                result.BallsSunk = sunk.Select(rack.Ball).ToList();
                result.Type = EightBallPoolRules.ValidateShot(rack, result, isBreak);
                rack.SinkBalls(sunk, result.LegalPot);
                if (result.BallsSunk.Any() && result.LegalPot)
                {
                    result.FirstLegalPot = true;
                    team.Shooting = result.BallsSunk.First().Class;
                }
            }
            return result;
        }
    }

    public enum Game
    {
        EightBallPool
    }
}