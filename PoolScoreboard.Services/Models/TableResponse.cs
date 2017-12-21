using PoolScoreboard.Application;

namespace PoolScoreboard.Services.Models
{
    public class TableResponse
    {
        public IRack Rack { get; set; }
        public FrameResponse Frame { get; set; }
        public TeamResponse CurrentShooter { get; set; }
        public TeamResponse WaitingShooter { get; set; }
        
        public TableResponse(Table table)
        {
            Rack = table.Rack;
            Frame = new FrameResponse(table.CurrentFrame);
            CurrentShooter = new TeamResponse(Table.CurrentShooter);
        }
    }
}