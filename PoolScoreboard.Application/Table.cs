using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using PoolScoreboard.Common;

namespace PoolScoreboard.Application
{
    public class Table
    {
        public static ITeam CurrentShooter { get; set; }
        private static ITeam Team1 { get; set; }
        private static ITeam Team2 { get; set; }
        
        private readonly IRack _balls;
        public static readonly IBall cueBall = new CueBall();
        
        public Table(IRack balls, IEnumerable<IPlayer> team1, IEnumerable<IPlayer> team2)
        {
            _balls = balls;
        }
        
        public bool CalculateShotLegality()
        {
            return true;
        }
    }
}