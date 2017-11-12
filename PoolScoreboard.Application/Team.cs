using System.Collections.Generic;
using System.Linq;

namespace PoolScoreboard.Application
{
    public interface ITeam
    {
        List<IPlayer> Players { get; set; }
        bool Breaker { get; set; }
        int ShotCount { get; }
        
        ShotResult TakeShot(List<IBall> sunk);
    }
    
    public abstract class Team : ITeam
    {
        public List<IPlayer> Players { get; set; }
        public bool Breaker { get; set; }
        public int ShotCount { get; private set; } = 0;

        public IPlayer ThisShooter => Players[LastShooterIndex];
        public IPlayer NextShooter => Players[LastShooterIndex++];
        
        protected int LastShooterIndex;
        
        protected Team(IEnumerable<IPlayer> players, int firstShooter = 0)
        {
            Players = players.ToList();
            LastShooterIndex = firstShooter;
        }
        
        public ShotResult TakeShot(List<IBall> sunk)
        {
            ShotCount++;
            var result = new ShotResult
            {
                ShotBy = this,
                Shooter = ThisShooter,
                BallsSunk = sunk
            };
            return result;
        }
        
        public BallClass Shooting { get; set; }
        
        protected void UpdateLastShooterIndex()
        {
            if (++LastShooterIndex >= Players.Count) LastShooterIndex = 0;
        }
    }
    
    public class EightBallPoolTeam : Team
    {
        public EightBallPoolTeam(IEnumerable<IPlayer> players, int firstShooter = 0) :
            base(players, firstShooter)
        { }
    }
}