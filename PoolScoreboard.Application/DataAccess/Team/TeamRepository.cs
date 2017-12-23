using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PoolScoreboard.Application.Interfaces;

namespace PoolScoreboard.Application.DataAccess.Team
{
    public interface ITeamRepository<T> : IRepository<T> where T : ITeam
    {
        
    }

    public interface IEightBallTeamRepository : ITeamRepository<EightBallPoolTeam>
    {
        
    }
    
    public class EightBallTeamRepository : IEightBallTeamRepository
    {
        public EightBallPoolTeam Fetch(int id)
        {
            return new EightBallPoolTeam(null, 0);
        }

        public EightBallPoolTeam Save(EightBallPoolTeam team)
        {
            return new EightBallPoolTeam(null, 0);
        }

        public List<EightBallPoolTeam> List(Expression<Func<EightBallPoolTeam, bool>> whereCondition)
        {
            return new List<EightBallPoolTeam>();
        }

        public List<EightBallPoolTeam> List()
        {
            return new List<EightBallPoolTeam>();
        }

        public void Delete(EightBallPoolTeam team)
        {
            Delete(team.Id);
        }

        public void Delete(int id)
        {
            
        }
    }
}