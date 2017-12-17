using PoolScoreboard.Application;

namespace PoolScoreboard.Tests
{
    public class EightBallRackTestWrapper : EightBallPoolRack
    {
        public EightBallRackTestWrapper(bool isBreak = false, bool sinkAll = false, bool sinkAllSolids = false)
        {
            if (!isBreak)
                SinkBall("2", true);
            if (sinkAllSolids)
                SinkAllSolids();
            if (sinkAll)
                SinkAllColours();
        }
        
        public void SinkBall(string identifier, bool legal)
        {
            base.SinkBall(identifier, legal);
        }
        
        private void SinkAllColours()
        {
            for (int i = 1; i <= 15; i++)
                if (i != 8) SinkBalls(new string[] { i.ToString() }, true);
        }
        
        private void SinkAllSolids()
        {
            for (int i = 1; i <= 7; i++)
                if (i != 8) SinkBalls(new string[] { i.ToString() }, true);
        }
    }
}