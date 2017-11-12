using System.Collections.Generic;
using System.Linq;

namespace PoolScoreboard.Application
{
    public interface ITeam
    {
        List<IPlayer> Players { get; set; }
        bool Breaker { get; set; }
        int ShotCount { get; set; }
    }
    
    public class Team : ITeam
    {
        public List<IPlayer> Players { get; set; }
        public bool Breaker { get; set; }
        public int ShotCount { get; set; }
        
        public IPlayer ThisShooter => Players[_lastShooterIndex];
        public IPlayer NextShooter => Players[_lastShooterIndex++];

        protected int _lastShooterIndex;
        
        public Team(IEnumerable<IPlayer> players, int firstShooter = 0)
        {
            Players = players.ToList();
            _lastShooterIndex = firstShooter;
        }
        
        protected void UpdateLastShooterIndex()
        {
            if (++_lastShooterIndex >= Players.Count) _lastShooterIndex = 0;
        }
    }
}