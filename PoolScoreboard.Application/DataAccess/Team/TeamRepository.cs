using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Linq.Expressions;
using PoolScoreboard.Application.DataAccess.AppContext;
using PoolScoreboard.Application.DataAccess.User;
using PoolScoreboard.Application.Interfaces;

namespace PoolScoreboard.Application.DataAccess.Team
{
    public interface IPoolTeamRepository : IRepository<EightBallPoolTeam>
    {
        
    }

    public class PoolTeamRepository : Repository<EightBallPoolTeam, TeamDto>, IPoolTeamRepository
    {
        private readonly IPlayerRepository _playerRepository = new PlayerRepository();

        public override EightBallPoolTeam Save(EightBallPoolTeam item)
        {
            var team = base.Save(item);
            SavePlayers(team.Players);
            return team;
        }
        
        protected override EightBallPoolTeam CastFromDto(TeamDto dto)
        {
            var result = new EightBallPoolTeam
            {
                Id = dto.Id,
                Players = GetPlayersFromCsv(dto.PlayersCsv)
            };
            SavePlayers(result.Players);
            return result;
        }

        private List<Player> GetPlayersFromCsv(string csv)
        {
            var players = csv.Split(',');
            return _playerRepository.List().Where(x => players.Any(p => p == x.Id.ToString())).ToList();
        }

        private void SavePlayers(List<Player> players)
        {
            players.ForEach(p => _playerRepository.Save(p));
        }
    }
}