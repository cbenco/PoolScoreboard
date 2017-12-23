using System;
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

        void Sink(bool legal);
    }
    
    public abstract class Ball : IBall
    {
        public virtual string Identifier { get; protected set; }
        public virtual bool OnTable { get; set; }
        public virtual ITeam SunkBy { get; set; }
        public virtual bool LegallySunk { get; set; }
        public virtual BallClass Class { get; }
        
        public virtual void Sink(bool legal)
        {
            SunkBy = Table.CurrentShooter;
            OnTable = false;
            LegallySunk = legal;
        }
    }
    
    public class CueBall : Ball
    {
        public override string Identifier => Constants.BallNames.CueBall;
        public override BallClass Class => BallClass.Neither;
        public override bool LegallySunk { get; set; } = false;
    }
    
    public sealed class PoolBall : Ball
    {
        public int Number => int.Parse(Identifier);
        public override BallClass Class
        {
            get
            {
                if (Number < 8) return BallClass.Solids;
                return Number > 8 ? BallClass.Stripes : BallClass.EightBall;
            }
        }
        
        public PoolBall(string identifier, bool legallySunk = false)
        {
            if (IsLegalIdentifier(identifier, out var number) && number < 1 || number > 15) 
                throw new ArgumentException("Ball number is out of range.");
            if (!IsLegalIdentifier(identifier) && identifier != Table.CueBall.Identifier)
                throw new ArgumentException("Invalid ball identifier.");
            
            Identifier = identifier;
            OnTable = true;
            LegallySunk = legallySunk;
            SunkBy = null;
        }

        private bool IsLegalIdentifier(string identifier, out int number)
        {
            return int.TryParse(identifier, out number);
        }

        private bool IsLegalIdentifier(string identifier)
        {
            return int.TryParse(identifier, out var number);
        }
        
        public override void Sink(bool legal)
        {
            SunkBy = Table.CurrentShooter;
            OnTable = false;
            LegallySunk = legal;
        }
    }
    
    public enum BallClass
    {
        Neither,
        Solids,
        Stripes,
        EightBall,
        Red,
        Colour
    }
}