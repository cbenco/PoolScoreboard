using System.Collections.Generic;
using PoolScoreboard.Application;

namespace PoolScoreboard.Services.Models
{
    public class TeamResponse
    {
        public IEnumerable<PlayerResponse> Players { get; set; }
        public int CurrentShooterId { get; set; }

        public TeamResponse(ITeam team)
        {
            Players = PlayerResponse.CreateMany(team.Players, team.ThisShooter.Id.Value);
            CurrentShooterId = team.ThisShooter.Id.Value;
        }
    }
}