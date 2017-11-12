﻿using System;
using System.Dynamic;
using System.Linq.Expressions;
using PoolScoreboard.Common;

namespace PoolScoreboard.Application
{
    public interface IBall
    {
        string Identifier { get; }
        bool OnTable { get; set; }
        ITeam SunkBy { get; set; }
        bool LegallySunk { get; set; }
        BallClass Class { get; }

        void Sink();
    }
    
    public abstract class Ball : IBall
    {
        public virtual string Identifier { get; protected set; }
        public virtual bool OnTable { get; set; }
        public virtual ITeam SunkBy { get; set; }
        public virtual bool LegallySunk { get; set; }
        public virtual BallClass Class { get; }

        public virtual void Sink()
        {
            SunkBy = Table.CurrentShooter;
            OnTable = false;
        }
    }
    
    public class CueBall : Ball
    {
        public override string Identifier => Constants.BallNames.CueBall;
        public override bool LegallySunk { get; set; } = false;
    }
    
    public sealed class PoolBall : Ball
    {
        private int Number => int.Parse(Identifier);
        public override BallClass Class
        {
            get
            {
                if (Number < 8) return BallClass.Solids;
                return Number > 8 ? BallClass.Stripes : BallClass.EightBall;
            }
        }
        
        public PoolBall(int number)
        {
            if (number < 1 || number > 15) throw new ArgumentException("Ball number is out of range.");
            
            Identifier = number.ToString();
            OnTable = true;
            LegallySunk = false;
            SunkBy = null;
        }
    }
    
    public enum BallClass
    {
        Solids,
        Stripes,
        EightBall,
        Red,
        Colour
    }
}