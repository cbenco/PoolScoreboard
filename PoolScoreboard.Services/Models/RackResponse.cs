using System.Collections.Generic;
using PoolScoreboard.Application;

namespace PoolScoreboard.Services.Models
{
    public class RackResponse
    {
        public IEnumerable<string> SinkableIds { get; set; }
        public IEnumerable<string> OffTableIds { get; set; }
    }

    public class EightBallRackResponse : RackResponse
    {
        public EightBallRackResponse(EightBallPoolRack rack)
        {
            SinkableIds = rack.GetSinkableBalls;
            OffTableIds = rack.GetSunkBalls;
        }
    }
}