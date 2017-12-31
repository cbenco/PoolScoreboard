using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using PoolScoreboard.Application.DataAccess.AppContext;
using PoolScoreboard.Application.Interfaces;

namespace PoolScoreboard.Application.DataAccess.Team
{
    public interface ITeamRepository : IRepository<ITeam>
    {
        
    }

    public class TeamRepository : ITeamRepository
    {
        public ITeam Fetch(int id)
        {
            return new EightBallPoolTeam(null);
        }
        
        public List<ITeam> List(Expression<Func<ITeam, bool>> whereCondition)
        {
            return new List<ITeam>();
        }

        public List<ITeam> List()
        {
            return new List<ITeam>();
        }
        
        public ITeam Save(ITeam team)
        {
            var dto = new TeamDto(team);

            using (var db = new ScoreboardDatabase())
            {
                if (!dto.Id.HasValue)
                {
                    db.Teams.Add(dto);
                    team.Id = dto.Id;
                }
                SwapTeams(db.Teams, dto);
                db.SaveChanges();
            }
            
            return team;
        }
        
        private void SwapTeams(DbSet<TeamDto> db, TeamDto shot)
        {
            var resultFromDb = GetById(db, shot.Id ?? 0);
            if (shot.Id.HasValue && shot.Id.Value != 0)
                db.Remove(resultFromDb);
            db.Add(shot);
        }
        
        private TeamDto GetById(IQueryable<TeamDto> db, int id)
        {
            return db.FirstOrDefault(result => result.Id == id);
        }
        
        public void Delete(ITeam team)
        {
            if (team.Id.HasValue)
                Delete(team.Id.Value);
        }

        public void Delete(int id)
        {
            
        }
    }
}