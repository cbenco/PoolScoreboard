using System;
using PoolScoreboard.Application.DataAccess.Match;
using PoolScoreboard.Application.DataAccess.Shot;
using PoolScoreboard.Application.DataAccess.Team;
using PoolScoreboard.Application.DataAccess.User;

namespace PoolScoreboard.Application.DataAccess.AppContext
{
    public interface IEntityDtoFactory
    {
        EntityDto Create(object argument);
    }
    public class EntityDtoFactory : IEntityDtoFactory 
    {
        public EntityDto Create(object argument)
        {
            var type = argument.GetType();
            if (type == typeof(Type))
                type = (Type)argument;
            if (type == typeof(Application.Team))
                return new TeamDto((EightBallPoolTeam)argument);
            if (type == typeof(ShotResult))
                return new ShotResultDto((ShotResult)argument);
            if (type == typeof(Frame))
                return new FrameDto((Frame)argument);
            if (type == typeof(Player))
                return new PlayerDto((Player)argument);
            
            throw new NotImplementedException();
        }
    }
}