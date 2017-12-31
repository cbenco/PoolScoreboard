using System.Collections.Generic;
using System.Linq;
using PoolScoreboard.Application.Interfaces;

namespace PoolScoreboard.Application
{
    public interface ITeam
    {
        int? Id { get; set; }
        Frame Frame { get; set; }
        List<Player> Players { get; set; }
        string PlayersCsv { get; }
        bool Breaker { get; set; }
        int ShotCount { get; }
        IPlayer ThisShooter { get; }
        BallClass Class { get; set; }
        BallClass Opposite { get; }
    }
    
    public abstract class Team : ISaveable, ITeam
    {
        public int? Id { get; set; }
        public List<Player> Players { get; set; }
        public bool Breaker { get; set; }
        public int ShotCount { get; protected set; } = 0;
        public bool HasColours { get; set; }
        public Frame Frame { get; set; }

        public string PlayersCsv
        {
            get
            {
                var s = Players[0].Id.ToString();
                for (var i = 1; i < Players.Count; i++)
                    s += "," + Players[i].Id;
                return s;
            }
        }
        
        public Team() {}
        
        public IPlayer ThisShooter => Players[LastShooterIndex];
        public IPlayer NextShooter => Players[LastShooterIndex++];
        
        protected int LastShooterIndex;
        
        public Team(IEnumerable<Player> players, int firstShooter = 0)
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
        public EightBallPoolTeam() {}
        public EightBallPoolTeam(IEnumerable<Player> players, int firstShooter = 0) :
            base(players, firstShooter)
        {
            HasColours = true;
        }
    }
}