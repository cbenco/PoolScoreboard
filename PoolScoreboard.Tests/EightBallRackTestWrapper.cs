using PoolScoreboard.Application;

namespace PoolScoreboard.Tests
{
    public class EightBallRackTestWrapper : EightBallPoolRack
    {
        public void SinkBall(string identifier, bool legal)
        {
            base.SinkBall(identifier, legal);
        }
    }
}