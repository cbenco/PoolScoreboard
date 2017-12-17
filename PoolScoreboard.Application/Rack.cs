using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using PoolScoreboard.Common;

namespace PoolScoreboard.Application
{
    /// <summary>
    /// Holds all the shootable balls on the table.
    /// Does not include the cue ball.
    /// </summary>
    public interface IRack
    {
        void SinkBalls(IEnumerable<string> identifiers, bool legal);
        IBall Ball(string identifier);
        bool IsBreak { get; }
        bool GameOver { get; }
        bool HasNoColours { get; }
        bool HasNo(BallClass colour);
    }
    
    public abstract class Rack<T> : List<T>, IRack where T : IBall
    {
        public IBall Ball(string identifier)
        {
            if (!LegalIdentifier(identifier))
            {
                throw new ArgumentException("Invalid ball identifier.");
            }
            if (identifier == Constants.BallNames.CueBall)
                return Table.CueBall;
            
            return this.FirstOrDefault(b => b.Identifier == identifier);
        }
        public virtual void SinkBalls(IEnumerable<string> identifiers, bool legal)
        {
            foreach(var identifier in identifiers)
                SinkBall(identifier, legal);
        }
        
        protected void SinkBall(string identifier, bool legal)
        {
            Ball(identifier).Sink(legal);
        }

        public virtual bool IsBreak { get; }
        public virtual bool GameOver { get; }
        public virtual bool HasNoColours { get; }
        public virtual bool HasAllColours { get; }

        public virtual bool HasNo(BallClass colour)
        {
            if (colour == BallClass.Neither)
                return false;
            return this.Where(b => b.Class == colour).All(b => !b.OnTable);
        }
        
        protected abstract bool LegalIdentifier(string identifier);
    }
    
    public class EightBallPoolRack : Rack<PoolBall>
    {
        public EightBallPoolRack()
        {
            for (var i = 1; i <= Constants.NumberOfBalls.EightBall; i++)
            {
                Add(new PoolBall(i));
            }
        }

        public override bool IsBreak => this.Count(b => b.OnTable) == 15;
        public override bool GameOver => !this.First(b => b.Number == 8).OnTable;
        
        public bool OpenTable => this.All(b => b.OnTable || !b.LegallySunk);
        
        public override bool HasAllColours => this.Count(b => b.Class == BallClass.Solids && b.OnTable) == 7 &&
                                     this.Count(b => b.Class == BallClass.Stripes && b.OnTable) == 7;
        
        public override bool HasNoColours => HasNo(BallClass.Solids) && HasNo(BallClass.Stripes);
        
        protected override bool LegalIdentifier(string identifier)
        {
            return
                identifier == Constants.BallNames.CueBall ||
                int.TryParse(identifier, out var result) ||
                result > 0 ||
                result < Constants.NumberOfBalls.EightBall;
        }
    }
}
