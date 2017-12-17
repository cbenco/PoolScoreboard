using System;
using System.Collections.Generic;
using System.Linq;

namespace PoolScoreboard.Application
{
    public class Frame
    {
        public DateTime Start { get; set; }
        
        public ITeam Winner { get; set; }
        public ITeam Loser { get; set; }
        
        private List<ShotResult> _shots;
        public List<ShotResult> Shots => _shots ?? (_shots = new List<ShotResult>());
        
        private List<IPlayer> _players;
        public List<IPlayer> Players
        {
            get => _players ?? (_players = new List<IPlayer>());
            set => _players = value;
        }
        
        private List<IBall> _sinkableBalls;
        public List<IBall> Sinkable
        {
            get => _sinkableBalls ?? (_sinkableBalls = new List<IBall>());
            set => _sinkableBalls = value;
        }
        
        public List<IBall> Sunk => _shots.SelectMany(x => x.BallsSunk).ToList();
    }
}