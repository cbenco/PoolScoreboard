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
        BallClass Class { get; set; }
        BallClass Opposite { get; }
    }
    
    public abstract class Team : ITeam
    {
        public int Id { get; set; }
        public List<IPlayer> Players { get; set; }
        public bool Breaker { get; set; }
        public int ShotCount { get; protected set; } = 0;
        public bool HasColours { get; set; }
        
        public IPlayer ThisShooter => Players[LastShooterIndex];
        public IPlayer NextShooter => Players[LastShooterIndex++];
        
        protected int LastShooterIndex;
        
        public Team(IEnumerable<IPlayer> players, int firstShooter = 0)
        {
            Players = players.ToList();
            LastShooterIndex = firstShooter;
        }

        private BallClass _class;
        public BallClass Class
        {
            get => HasColours ? _class : BallClass.EightBall;
            set
            {
                if (value == BallClass.EightBall)
                    HasColours = false;
                else _class = value;
            }
        }
        
        public BallClass Opposite
        {
            get
            {
                if (Class != BallClass.Neither)
                    return Class == BallClass.Solids ? BallClass.Stripes : BallClass.Solids;
                return BallClass.Neither;
            }
        }
        
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
            HasColours = true;
        }
    }
}