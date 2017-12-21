using System;
using PoolScoreboard.Application;
using PoolScoreboard.Services.Models;

namespace PoolScoreboard.Services.Factories
{
    public interface IRackResponseFactory
    {
        RackResponse Create(Game game, IRack rack);
    }
    
    public class RackResponseFactory : IRackResponseFactory
    {
        public RackResponse Create(Game game, IRack rack)
        {
            switch (game)
            {
                case Game.EightBallPool:
                    return new EightBallRackResponse((EightBallPoolRack) rack);
                default:
                    throw new NotImplementedException();
            }
        }
    }

    public enum RackResponseType
    {
        EightBallRack
    }
}