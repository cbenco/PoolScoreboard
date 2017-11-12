using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using PoolScoreboard.Common;

namespace PoolScoreboard.Application
{
    public interface IRack
    {
        void SinkBall(string identifier);
    }
    
    public abstract class Rack<T> : List<IBall>, IRack
    {
        private IBall Ball(string identifier)
        {
            if (LegalIdentifier(identifier))
            {
                throw new ArgumentException("Invalid ball identifier.");
            }
            
            return this.FirstOrDefault(b => b.Identifier == identifier);
        }
        
        public virtual void SinkBall(string identifier)
        {
            Ball(identifier).Sink();
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
        
        public bool OpenTable => this.All(b => b.OnTable || !b.LegallySunk);
        
        protected override bool LegalIdentifier(string identifier)
        {
            return !int.TryParse(identifier, out var result) || 
                   result < 0 || 
                   result > Constants.NumberOfBalls.EightBall;
        }
    }
}
