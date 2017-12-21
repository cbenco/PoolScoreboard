using System.Collections.Generic;

namespace PoolScoreboard.Services.Models
{
    public class RackResponse
    {
        public IEnumerable<string> SinkableIds { get; set; }
        public IEnumerable<string> OffTableIds { get; set; }
    }
}