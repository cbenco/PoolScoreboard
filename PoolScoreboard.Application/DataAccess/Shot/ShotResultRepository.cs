using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using PoolScoreboard.Application.DataAccess.AppContext;
using PoolScoreboard.Application.DataAccess.Match;
using PoolScoreboard.Application.DataAccess.Team;
using PoolScoreboard.Application.DataAccess.User;
using PoolScoreboard.Application.Interfaces;

namespace PoolScoreboard.Application.DataAccess.Shot
{
    public interface IShotResultRepository : IRepository<ShotResult>
    {
        
    }
    
    public class ShotResultRepository : Repository<ShotResult, ShotResultDto>, IShotResultRepository
    {
        private readonly IPlayerRepository _playerRepository = new PlayerRepository();
        private readonly IFrameRepository _frameRepository = new FrameRepository();
        private readonly IPoolTeamRepository _teamRepository = new PoolTeamRepository();

        protected override ShotResult CastFromDto(ShotResultDto dto)
        {
            var result = new ShotResult
            {
                Id = dto.Id,
                Frame = _frameRepository.Fetch(dto.FrameId),
                Shooter = _playerRepository.Fetch(dto.Shooter),
                //ShootingTeam = _teamRepository.Fetch(dto.ShootingTeamId ?? 0),
                Type = (ShotResultType) Enum.Parse(typeof(ShotResultType), dto.Type)
            };
            return result;
        }
    }
}