using System;
using System.Security.Permissions;
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
            var type = argument.GetType().Name;
            if (type == "Team" || type == "EightBallPoolTeam")
                return new TeamDto((EightBallPoolTeam)argument);
            if (type == "ShotResult" || type == "PoolShotResult")
                return new ShotResultDto((ShotResult)argument);
            if (type == "Frame")
                return new FrameDto((Frame)argument);
            if (type == "Player")
                return new PlayerDto((Player)argument);
            
            throw new NotImplementedException();
        }
    }
}