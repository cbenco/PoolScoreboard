using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Xml.XPath;

namespace PoolScoreboard.Application
{
    public class ShotResult
    {
        public Team ShotBy { get; set; }
        public IPlayer Shooter { get; set; }
        public  IBall ObjectBall { get; set; }
        public List<IBall> BallsSunk { get; set; }
        public bool Legal => Type == ShotResultType.Legal;
        public ShotResultType Type { get; set; }
    }
    
    public class PoolShotResult : ShotResult
    {
    }
    
    public enum ShotResultType
    {
        Legal,
        WrongObjectBall,
        WentInOff,
        Scratch
    }
}