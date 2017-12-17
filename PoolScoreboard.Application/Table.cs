using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using PoolScoreboard.Common;

namespace PoolScoreboard.Application
{
    public class Table
    {
        private Game _game = Game.EightBallPool;
        public static ITeam CurrentShooter { get; set; }
        private static ITeam Team1 { get; set; }
        private static ITeam Team2 { get; set; }

        public Frame CurrentFrame;
        public List<Frame> Frames { get; set; }

        private readonly IRack _balls;
        public static readonly IBall CueBall = new CueBall();

        public bool GameOver => _balls.GameOver;
        
        public Table(IRack balls, ITeam team1, ITeam team2)
        {
            _balls = balls;
            Team1 = team1;
            Team2 = team2;

            CurrentShooter = team1;
            Frames = new List<Frame>();
            CurrentFrame = new Frame
            {
                Players = Team1.Players.Union(Team2.Players).ToList()
            };
            Frames.Add(CurrentFrame);
        }
        
        public ShotResult PlayShot(string objectBall, IEnumerable<string> sunk)
        {
            var shotResultFactory = new ShotResultFactory();
            var shotResult = shotResultFactory.Create(_game, CurrentShooter, 
                                                      _balls, objectBall, sunk);
            var teamNotShooting = GetTeamNotShooting();
            if (shotResult.FirstLegalPot)
            {
                teamNotShooting.Shooting = CurrentShooter.Opposite;
            }
            if (!shotResult.LegalPot)
                //Cycle teams
                CurrentShooter = teamNotShooting;
            
            CueBall.OnTable = true;
            CurrentFrame.Shots.Add(shotResult);
            return shotResult;
        }
        
        private ITeam GetTeamNotShooting()
        {
            return CurrentShooter == Team1 ? Team2 : Team1;
        }

        public override string ToString()
        {
            var result = Team1.ToString();
            result += Team2.ToString();
            return result;
        }
    }
}