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
            var rack = new EightBallRackTestWrapper();
            
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

        [Test]
        public void test_open_table_when_ball_illegally_sunk()
        {
            var rack = new EightBallRackTestWrapper();
            rack.SinkBall("1", false);
            
            Assert.IsTrue(rack.OpenTable);
        }

        [Test]
        public void test_hasnocolours_calculates_correctly()
        {
            var rack = new EightBallRackTestWrapper();
            
        }
    }
}