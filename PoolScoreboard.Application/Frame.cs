using System;
using System.Collections.Generic;

namespace PoolScoreboard.Application
{
    public class Frame
    {
        public DateTime Start { get; set; }
        private List<ShotResult> _shots;
        public List<ShotResult> Shots => _shots ?? (_shots = new List<ShotResult>());
    }
}