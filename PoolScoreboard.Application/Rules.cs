using System.Collections.Generic;
using System.Linq;

namespace PoolScoreboard.Application
{
    public class Rules
    {
        
    }
    
    public class EightBallPoolRules : Rules
    {
        public static ShotResultType ValidateShot(IRack rack, ShotResult result, bool isBreak)
        {
            var ballsSunk = result.BallsSunk.ToList();
            if (SunkBothTypes(ballsSunk))
                return isBreak ? ShotResultType.SunkBothOnBreak : ShotResultType.SunkOpponentsBall;
            if (isBreak)
                return ShotResultType.LegalPot;
            if (Scratch(result.ObjectBall)) 
                return ShotResultType.Scratch;
            if (WrongObjectBall((EightBallPoolRack)rack, result.ObjectBall, result.ShootingTeam)) 
                return ShotResultType.WrongObjectBall;
            if (WentInOff(ballsSunk)) 
                return ShotResultType.WentInOff;
            if (SunkOpponentBall(ballsSunk, result.ShootingTeam))
                return ShotResultType.SunkOpponentsBall;
            if (SunkNone(ballsSunk))
                return ShotResultType.Missed;
            return ShotResultType.LegalPot;
        }
        
        private static bool WrongObjectBall(EightBallPoolRack rack, IBall objectBall, ITeam shooter)
        {
            return !rack.OpenTable && objectBall.Class != shooter.Shooting;
        }
        
        private static bool WentInOff(ICollection<IBall> ballsSunk)
        {
            return ballsSunk.Contains(Table.cueBall);
        }

        private static bool SunkNone(ICollection<IBall> ballsSunk)
        {
            return !ballsSunk.Any();
        }

        private static bool SunkOpponentBall(IEnumerable<IBall> ballsSunk, ITeam shooter)
        {
            return ballsSunk.Any(b => b.Class != shooter.Shooting);
        }
        
        private static bool SunkBothTypes(ICollection<IBall> ballsSunk)
        {
            return ballsSunk.Any(b => b.Class == BallClass.Solids) &&
                   ballsSunk.Any(b => b.Class == BallClass.Stripes);
        }
        
        private static bool Scratch(IBall objectBall)
        {
            return objectBall == null;
        }
    }
}