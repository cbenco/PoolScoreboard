using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PoolScoreboard.Application;
using PoolScoreboard.Application.DataAccess.Shot;

namespace PoolScoreboard.Tests
{
    public class ShotResultRepositoryTests
    {
        [Test]
        [TestCase(new[] {"1"}, new[] {true}, "1|t")]
        [TestCase(new[] {"1"}, new[] {false}, "1|f")]
        [TestCase(new[] {"CUE"}, new[] {false}, "CUE|f")]
        [TestCase(new[] {"1", "2"}, new[] {true, true}, "1|t,2|t")]
        [TestCase(new[] {"1", "9"}, new[] {true, false}, "1|t,9|f")]
        [TestCase(new[] {"1", "9", "CUE"}, new[] {true, false, false}, "1|t,9|f,CUE|f")]
        
        public void test_that_convert_to_csv_returns_correct_string(string[] balls, bool[] legallySunk, string expected)
        {
            var repository = new ShotResultTestWrapper
            {
                BallsSunk = SetUpBalls(balls, legallySunk)
            };

            var result = repository.ConvertToCsv();
            
            Assert.That(result == expected);
        }

        [Test]
        [TestCase("1|t")]
        [TestCase("1|f")]
        [TestCase("CUE|f")]
        [TestCase("1|2,9|f")]
        [TestCase("1|2,9|f,CUE|f")]
        public void test_that_convert_csv_returns_correct_balls(string balls)
        {
            var repository = new ShotResultTestWrapper();

            var result = repository.ConvertPoolBallsFromStrings(balls).ToList();
            var ballCodes = balls.Split(',');
            for (int i = 0; i > ballCodes.Length; i++)
            {
                var strings = balls.Split('|');
                var id = strings[0];
                var legal = strings[1] == "t";
                var ball = (Ball)result.Where(b => b.Identifier == id);
                
                Assert.That(ball.Identifier == strings[result.IndexOf(ball)]);
                Assert.That(ball.LegallySunk == legal);
            }
        }

        private IEnumerable<IBall> SetUpBalls(string[] balls, bool[] legallySunk)
        {
            return balls.Select((t, i) => new PoolBall(t, legallySunk[i])).ToList();
        }
        
        private class ShotResultTestWrapper : ShotResult
        {
            public new string ConvertToCsv()
            {
                return base.ConvertBallsSunkToCsv();
            }

            public new IEnumerable<IBall> ConvertPoolBallsFromStrings(string balls)
            {
                return base.ConvertPoolBallsFromStrings(balls);
            }
        }   
    }
}