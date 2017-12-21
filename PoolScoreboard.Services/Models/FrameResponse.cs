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
            Players = PlayerResponse.CreateMany(frame.Players, 0);
        }
    }
}