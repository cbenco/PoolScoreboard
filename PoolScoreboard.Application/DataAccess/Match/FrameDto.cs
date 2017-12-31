using System.ComponentModel.DataAnnotations;
using System.Globalization;
using PoolScoreboard.Application.DataAccess.AppContext;

namespace PoolScoreboard.Application.DataAccess.Match
{
    public class FrameDto : EntityDto
    {
        public string Start { get; set; }
        
        public FrameDto() {}
        
        public FrameDto(Frame frame)
        {
            Id = frame.Id;
            Start = frame.Start.ToString(CultureInfo.CurrentCulture);
        }
    }
}