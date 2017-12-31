using System.ComponentModel.DataAnnotations;
using PoolScoreboard.Application.DataAccess.AppContext;

namespace PoolScoreboard.Application.DataAccess.Match
{
    public class FrameDto : EntityDto
    {
        public string Start { get; set; }
        
        public FrameDto() {}
        
        public FrameDto(Frame frame)
        {
            Start = frame.Start.ToShortTimeString();
        }
    }
}