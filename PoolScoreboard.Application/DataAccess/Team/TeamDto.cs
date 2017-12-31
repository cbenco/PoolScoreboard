using System;
using PoolScoreboard.Application.DataAccess.AppContext;

namespace PoolScoreboard.Application.DataAccess.Team
{
    public class TeamDto : EntityDto
    {
        public int? FrameId { get; set; }
        public string PlayersCsv { get; set; }

        public TeamDto() {}
        
        public TeamDto(ITeam team)
        {
            FrameId = team.Frame.Id;
            PlayersCsv = team.PlayersCsv;
        }
    }
}