using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PoolScoreboard.Application.DataAccess.AppContext;
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
        
        public ShotResult Fetch(int id)
        {
        }
        
        protected IEnumerable<IBall> GetBallsSunkFromCsv(string csv, int shootingTeamId)
        {
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
            return new ShotResult();
        }
        
        public void Delete(ShotResult shotResult)
        {
            
        }
    }
}