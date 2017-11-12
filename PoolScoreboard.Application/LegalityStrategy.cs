using System;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace PoolScoreboard.Application
{
    public interface ITurnStrategy
    {
        TurnResult EndOfTurn(IRack rack, ShotResult result);
    }
    
    public class EightBallPoolTurnStrategy : ITurnStrategy
    {
        public TurnResult EndOfTurn(IRack rack, ShotResult result)
        {
            return new TurnResult
            {
                Fouls = result.Type,
                NextShooter = GetNextShooter(rack, result)
            };
        }
        
        private static NextShooter GetNextShooter(IRack rack, ShotResult result)
        {
            return (result.BallsSunk.Any() && result.Type == ShotResultType.Legal) ? 
                NextShooter.Repeat : NextShooter.Next;
        }
    }
    
    public class TurnResult
    {
        public NextShooter NextShooter { get; set; }
        public ShotResultType Fouls { get; set; }
    }
    
    public enum NextShooter
    {
        Next,
        Repeat
    }
}