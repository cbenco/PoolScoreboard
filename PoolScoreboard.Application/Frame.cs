using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace PoolScoreboard.Application
{
    public class Frame
    {
        public DateTime Start { get; set; }
        private List<ShotResult> _shots;
        public List<ShotResult> Shots => _shots ?? (_shots = new List<ShotResult>());
        private List<IPlayer> _players;

        public List<IPlayer> Players
        {
            get => _players ?? (_players = new List<IPlayer>());
            set => _players = value;
        } 
    }
}