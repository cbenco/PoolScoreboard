using System.Collections.Generic;
using System.Runtime;
using NUnit.Framework;
using PoolScoreboard.Application;

namespace PoolScoreboard.Tests
{
    [TestFixture]
    public class TeamTests
    {
        [Test]
        [TestCase(4)]
        [TestCase(1)]
        [TestCase(50)]
        public void test_that_last_shooter_is_set_to_zero_on_overflow(int maxPlayers)
        {
            var team = SetupTeam(maxPlayers);

            for (var i = 1; i < maxPlayers; i++)
            {
                team.UpdateLastShooterIndex();
                Assert.That(team.LastShooterIndex == i);
            }
            team.UpdateLastShooterIndex();
            Assert.That(team.LastShooterIndex == 0);
        }

        [Test]
        [TestCase(BallClass.Solids, BallClass.Stripes)]
        [TestCase(BallClass.Stripes, BallClass.Solids)]
        [TestCase(BallClass.Neither, BallClass.Neither)]
        public void test_that_opposite_calculates_correctly(BallClass shooting, BallClass opposite)
        {
            var team = SetupTeam(1);
            team.Shooting = shooting;
            Assert.AreEqual(team.Opposite, opposite);
        }
        
        private static TeamTestWrapper SetupTeam(int numberOfPlayers)
        {
            var players = new List<Player>();
            for (int i = 0; i < numberOfPlayers; i++)
                players.Add(new Player
                    {Id = numberOfPlayers + 1});
            return new TeamTestWrapper(players);
        }
        
        private class TeamTestWrapper : Team
        {
            public TeamTestWrapper(IEnumerable<Player> players) : base(players) {}
            
            public int LastShooterIndex => base.LastShooterIndex;
            public void UpdateLastShooterIndex()
            {
                base.UpdateLastShooterIndex();
            }
        }
    }
}