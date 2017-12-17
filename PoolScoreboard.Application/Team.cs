using System.Collections.Generic;
using System.Linq;

namespace PoolScoreboard.Application
{
    public interface ITeam
    {
        int Id { get; set; }
        List<IPlayer> Players { get; set; }
        bool Breaker { get; set; }
        int ShotCount { get; }
        IPlayer ThisShooter { get; }
        BallClass Shooting { get; set; }
        BallClass Opposite { get; }
        ShotResult TakeShot(IEnumerable<IBall> sunk);
    }
    
    public abstract class Team : ITeam
    {
        public int Id { get; set; }
        public List<IPlayer> Players { get; set; }
        public bool Breaker { get; set; }
        public int ShotCount { get; protected set; } = 0;

        public IPlayer ThisShooter => Players[LastShooterIndex];
        public IPlayer NextShooter => Players[LastShooterIndex++];
        
        protected int LastShooterIndex = 0;
        
        protected Team(IEnumerable<IPlayer> players, int firstShooter = 0)
        {
            Players = players.ToList();
            LastShooterIndex = firstShooter;
        }
        
        public virtual ShotResult TakeShot(IEnumerable<IBall> sunk)
        {
            ShotCount++;
            var result = new ShotResult
            {
                ShootingTeam = this,
                Shooter = ThisShooter,
                BallsSunk = sunk
            };
            return result;
        }
        
        public BallClass Shooting { get; set; }
        public BallClass Opposite => 
            Shooting == BallClass.Solids ? BallClass.Stripes : BallClass.Solids;
        
        protected void UpdateLastShooterIndex()
        {
            if (++LastShooterIndex >= Players.Count) LastShooterIndex = 0;
        }
        
        public override string ToString()
        {
            var result = $"TEAM {Id}: \n";
            Players.ForEach(p => result += p.ToString() + "\n");
            result += $"Breaking: {Breaker}\n";
            return result;
        }
    }
    
    public class EightBallPoolTeam : Team
    {
        public EightBallPoolTeam(IEnumerable<IPlayer> players, int firstShooter = 0) :
            base(players, firstShooter)
        {
        }
        
        public override ShotResult TakeShot(IEnumerable<IBall> sunk)
        {
            ShotCount++;
            var result = new PoolShotResult()
            {
                ShootingTeam = this,
                Shooter = ThisShooter,
                BallsSunk = sunk
            };
            return result;
        }
    }
}