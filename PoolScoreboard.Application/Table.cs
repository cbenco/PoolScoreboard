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
        public ITeam Team1 { get; set; }
        public ITeam Team2 { get; set; }

        public Frame CurrentFrame;

        public IRack Rack { get; }
        public static readonly IBall CueBall = new CueBall();

        public bool GameOver => Rack.GameOver;
        
        public Table(IRack rack, ITeam team1, ITeam team2)
        {
            Rack = rack;
            Team1 = team1;
            Team2 = team2;

            CurrentShooter = team1;
            CurrentFrame = new Frame
            {
                Players = Team1.Players.Union(Team2.Players).ToList()
            };
        }
        
        public ShotResult PlayShot(string objectBall, IEnumerable<string> sunk)
        {
            var shotResultFactory = new ShotResultFactory();
            var shotResult = shotResultFactory.Create(_game, CurrentShooter, 
                                                      Rack, objectBall, sunk);
            ProcessShot(shotResult);
            CueBall.OnTable = true;
            
            CurrentFrame.Shots.Add(shotResult);
            return shotResult;
        }
        
        private ITeam GetTeamNotShooting => CurrentShooter == Team1 ? Team2 : Team1;
        
        private void ProcessShot(ShotResult result)
        {
            var teamNotShooting = GetTeamNotShooting;
            if (result.FirstLegalPot)
            {
                CurrentShooter.Class = result.ObjectBall.Class;
                teamNotShooting.Class = CurrentShooter.Opposite;
            }
            if (Rack.HasNo(CurrentShooter.Class))
            {
                CurrentShooter.Class = BallClass.EightBall;
            }
            if (!result.LegalPot)
                //Cycle teams
                CurrentShooter = teamNotShooting;
        }

        public override string ToString()
        {
            var result = Team1.ToString();
            result += Team2.ToString();
            return result;
        }
    }
}