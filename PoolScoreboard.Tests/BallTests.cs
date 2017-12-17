using System;
using NUnit.Framework;
using PoolScoreboard.Application;

namespace PoolScoreboard.Tests
{
    public class BallTests
    {
        [TestFixture]
        public class Tests
        {
            #region Ball creation
            [Test]
            [TestCase(1, BallClass.Solids)]
            [TestCase(7, BallClass.Solids)]
            [TestCase(8, BallClass.EightBall)]
            [TestCase(9, BallClass.Stripes)]
            [TestCase(10, BallClass.Stripes)]
            [TestCase(15, BallClass.Stripes)]
            public void test_that_new_pool_ball_returns_correct_ballclass(int number, BallClass expected)
            {
                var ball = new PoolBall(number);
                Assert.That(ball.Class == expected);
            }
            
            [Test]
            [TestCase(-12)]
            [TestCase(0)]
            [TestCase(16)]
            [TestCase(100)]
            public void test_that_new_pool_ball_with_number_outside_limit_throws_exception(int number)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    var ball = new PoolBall(number);
                });
            }
            #endregion
            
            #region Ball sinking
            
            #endregion
        }
    }
}