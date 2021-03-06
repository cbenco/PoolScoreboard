﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using PoolScoreboard.Application.DataAccess.Match;
using PoolScoreboard.Application.DataAccess.Shot;
using PoolScoreboard.Application.DataAccess.Team;
using PoolScoreboard.Application.Interfaces;
using PoolScoreboard.Common;

namespace PoolScoreboard.Application
{
    public class Table
    {
        private Game _game = Game.EightBallPool;
        public static ITeam CurrentShooter { get; set; }
        public ITeam Team1 { get; set; }
        public ITeam Team2 { get; set; }
        
        private IShotResultFactory _shotResultFactory = new ShotResultFactory();
        private IRepository<Frame> _frameRepository = new FrameRepository();
        private readonly IPoolTeamRepository _poolTeamRepository = new PoolTeamRepository();
        
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
                Team1 = team1,
                Team2 = team2
            };
            SaveCurrentFrame();
        }

        private void SaveCurrentFrame()
        {
            _frameRepository.Save(CurrentFrame);
            CurrentFrame.Team1.Frame = CurrentFrame;
            CurrentFrame.Team2.Frame = CurrentFrame;
            /*Team1 = _poolTeamRepository.Save((EightBallPoolTeam)CurrentFrame.Team1);
            Team2 = _poolTeamRepository.Save((EightBallPoolTeam)CurrentFrame.Team2);*/
        }

        public ShotResult PlayShot(string objectBall, IEnumerable<string> sunk)
        {
            var shotResult = _shotResultFactory.Create(_game, CurrentShooter, 
                                                      Rack, objectBall, sunk);
            ProcessShot(shotResult);
            
            CueBall.OnTable = true;
            
            CurrentFrame.Shots.Add(shotResult);
            SaveCurrentFrame();
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