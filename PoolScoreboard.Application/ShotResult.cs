using System.Collections.Generic;
using System.Linq;

namespace PoolScoreboard.Application
{
    public class ShotResult
    {
        public ITeam ShootingTeam { get; set; }
        public IPlayer Shooter { get; set; }
        public IBall ObjectBall { get; set; }
        public IEnumerable<IBall> BallsSunk { get; set; }
        public bool LegalPot => Type == ShotResultType.LegalPot || Type == ShotResultType.Win;
        public bool FirstLegalPot { get; set; }
        public ShotResultType Type { get; set; }

        public override string ToString()
        {
            var result = "";
            result += $"Shot by {Shooter.Name}";
            result += $"\nLegal | ";
            result = AppendList(result, BallsSunk.Where(b => b.LegallySunk));
            result += $"\nIllegal | ";
            result = AppendList(result, BallsSunk.Where(b => !b.LegallySunk));
            result += $"\nShot result: {Type.ToString()}";
            return result;
        }
        
        private string AppendList(string result, IEnumerable<IBall> balls)
        {
            foreach (var ball in balls)
            {
                result += $"{ball.Identifier} ";
            }
            return result;
        }
    }
    
    public class PoolShotResult : ShotResult
    {
    }
    
    public enum ShotResultType
    {
        Missed,
        LegalPot,
        WrongObjectBall,
        WentInOff,
        SunkBothOnBreak,
        SunkOpponentsBall,
        MissedObjectBall,
        Win,
        Loss
    }
}