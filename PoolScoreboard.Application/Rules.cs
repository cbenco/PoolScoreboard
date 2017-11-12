using System.Collections.Generic;

namespace PoolScoreboard.Application
{
    public class Rules
    {
        
    }
    
    public class EightBallPoolRules : Rules
    {
        public static ShotResultType ValidateShot(IRack rack, ShotResult result)
        {
            if (WrongObjectBall((EightBallPoolRack)rack, result.ObjectBall, result.ShotBy)) 
                return ShotResultType.WrongObjectBall;
            if (WentInOff(result.BallsSunk)) 
                return ShotResultType.WentInOff;
            return Scratch(result.ObjectBall) ? ShotResultType.Scratch : ShotResultType.Legal;
        }
        
        private static bool WrongObjectBall(EightBallPoolRack rack, IBall objectBall, Team shooter)
        {
            return !rack.OpenTable && objectBall.Class != shooter.Shooting;
        }
        
        private static bool WentInOff(ICollection<IBall> ballsSunk)
        {
            return ballsSunk.Contains(Table.cueBall);
        }
        
        private static bool Scratch(IBall objectBall)
        {
            return objectBall == null;
        }
    }
}