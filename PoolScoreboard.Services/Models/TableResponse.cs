﻿using PoolScoreboard.Application;
using PoolScoreboard.Services.Factories;

namespace PoolScoreboard.Services.Models
{
    public abstract class TableResponse
    {
        private readonly IRackResponseFactory _rackResponseFactory = new RackResponseFactory();
        
        public RackResponse Rack { get; set; }
        public FrameResponse Frame { get; set; }
        public TeamResponse CurrentShooter { get; set; }
        public TeamResponse WaitingShooter { get; set; }

        protected abstract RackResponseType RackType { get; }
        
        public TableResponse(Table table)
        {
            Rack = _rackResponseFactory.Create(Game.EightBallPool, table.Rack);
            Frame = new FrameResponse(table.CurrentFrame);
            CurrentShooter = new TeamResponse(Table.CurrentShooter);
        }
    }

    public class EightBallTableResponse : TableResponse
    {
        protected override RackResponseType RackType => RackResponseType.EightBallRack;

        public EightBallTableResponse(Table table) : base(table) {}
    }
}