using System.Collections.Generic;
using System.Linq;
using PoolScoreboard.Application;

namespace PoolScoreboard.Services.Models
{
    public class FrameResponse
    {
        IEnumerable<PlayerResponse> Players { get; set; }

        public FrameResponse(Frame frame)
        {
            Players = PlayerResponse.CreateMany(frame.Team1.Players.Concat(frame.Team2.Players), 0);
        }
    }
}