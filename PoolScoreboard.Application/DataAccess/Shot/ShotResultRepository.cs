using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using PoolScoreboard.Application.DataAccess.AppContext;
using PoolScoreboard.Application.DataAccess.Team;
using PoolScoreboard.Application.DataAccess.User;
using PoolScoreboard.Application.Interfaces;

namespace PoolScoreboard.Application.DataAccess.Shot
{
    public interface IShotResultRepository : IRepository<ShotResult>
    {
        
    }
    
    public class ShotResultRepository : IShotResultRepository
    {
        private readonly IPlayerRepository _playerRepository = new PlayerRepository();
        private readonly IEightBallTeamRepository _eightBallTeamRepository = new EightBallTeamRepository();
        
        public ShotResult Fetch(int id)
        {
            return new ShotResult();
        }
        
        public List<ShotResult> List(Expression<Func<ShotResult, bool>> whereCondition)
        {
            return new List<ShotResult>();
        }

        public List<ShotResult> List()
        {
            return new List<ShotResult>();
        }
        
        public ShotResult Save(ShotResult shotResult)
        {
            var dto = new ShotResultDto(shotResult);

            using (var db = new ScoreboardDatabase("public"))
            {
                if (!dto.Id.HasValue)
                {
                    db.ShotResults.Add(dto);
                    shotResult.Id = dto.Id;
                }
                SwapShotResults(db.ShotResults, dto);
                db.SaveChanges();
            }
            
            return shotResult;
        }

        private void SwapShotResults(DbSet<ShotResultDto> db, ShotResultDto shot)
        {
            var resultFromDb = GetById(db, shot.Id ?? 0);
            if (shot.Id.HasValue && shot.Id.Value != 0)
                db.Remove(resultFromDb);
            db.Add(shot);
        }
        
        protected ShotResultDto GetById(DbSet<ShotResultDto> db, int id)
        {
            return db.FirstOrDefault(result => result.Id == id);
        }
        
        public void Delete(ShotResult shotResult)
        {
            if (shotResult.Id.HasValue)
                Delete(shotResult.Id.Value);
        }

        public void Delete(int id)
        {
            
        }
    }
}