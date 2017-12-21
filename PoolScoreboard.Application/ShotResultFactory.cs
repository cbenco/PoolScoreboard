using System;
using System.Collections.Generic;
using System.Linq;

namespace PoolScoreboard.Application
{
    public interface IShotResultFactory
    {
        ShotResult Create(Game game, ITeam team, IRack rack, string objectBall, IEnumerable<string> sunk);
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
                result = new PoolShotResult
                {
                    ShootingTeam = team,
                    Shooter = team.ThisShooter,
                    ObjectBall = rack.Ball(objectBall),
                    BallsSunk = sunk.Select(rack.Ball).ToList()
                };
                result.Type = EightBallPoolRules.ValidateShot((EightBallPoolRack) rack, result);
                rack.SinkBalls(sunk, result.LegalPot);
                result.FirstLegalPot = result.BallsSunk.Any() && result.LegalPot;
            }
            else throw new NotImplementedException();
            return result;
        }
    }
    
    public enum Game
    {
        EightBallPool
    }
}