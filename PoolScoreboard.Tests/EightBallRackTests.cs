using NUnit.Framework;
using PoolScoreboard.Application;
using PoolScoreboard.Common;

namespace PoolScoreboard.Tests
{
    [TestFixture]
    public class EightBallRackTests
    {
        [Test]
        public void test_that_eight_ball_rack_contains_correct_number_of_balls()
        {
            EightBallPoolRack rack = new EightBallPoolRack();
            
            Assert.That(rack.Count == Constants.NumberOfBalls.EightBall);
        }
        
        [Test]
        public void test_open_table_when_no_ball_sunk()
        {
            var rack = new EightBallRackTestWrapper();
            
            Assert.IsTrue(rack.OpenTable);
        }
        
        [Test]
        public void test_open_table_when_ball_sunk()
        {
            var rack = new EightBallRackTestWrapper();
            rack.SinkBall("1", true);
            
            Assert.IsTrue(!rack.OpenTable);
        }
        
        private class EightBallRackTestWrapper : EightBallPoolRack
        {
            
        }
    }
}