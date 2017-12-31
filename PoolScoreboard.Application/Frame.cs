using System;
using System.Collections.Generic;
using System.Linq;
using PoolScoreboard.Application.Interfaces;

namespace PoolScoreboard.Application
{
    public class Frame : ISaveable
    {
        public int? Id { get; set; }
        
        public DateTime Start { get; set; }
        
        public ITeam Team1 { get; set; }
        public ITeam Team2 { get; set; }
        
        private List<ShotResult> _shots;
        public List<ShotResult> Shots => _shots ?? (_shots = new List<ShotResult>());
        
        private List<IBall> _sinkableBalls;
        public List<IBall> Sinkable
        {
            get => _sinkableBalls ?? (_sinkableBalls = new List<IBall>());
            set => _sinkableBalls = value;
        }
        
        public List<IBall> Sunk => _shots.SelectMany(x => x.BallsSunk).ToList();
    }
}