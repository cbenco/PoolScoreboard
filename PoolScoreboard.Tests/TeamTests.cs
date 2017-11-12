using System.Collections.Generic;
using NUnit.Framework;
using PoolScoreboard.Application;

namespace PoolScoreboard.Tests
{
    [TestFixture]
    public class TeamTests
    {
        [Test]
        public void test_that_last_shooter_is_set_to_zero_on_overflow()
        {
            var players = new List<Player>
            {
                new Player
                {
                    Id = 1,
                    Name = "Jim"
                },
                new Player
                {
                    Id = 1,
                    Name = "Tim"
                },
                new Player
                {
                    Id = 1,
                    Name = "Jon"
                },
                new Player
                {
                    Id = 1,
                    Name = "Sam"
                }
            };
            var team = new TeamTestWrapper(players);
            team.UpdateLastShooterIndex();
            Assert.That(team.LastShooterIndex == 1);
            team.UpdateLastShooterIndex();
            Assert.That(team.LastShooterIndex == 2);
            team.UpdateLastShooterIndex();
            Assert.That(team.LastShooterIndex == 3);
            team.UpdateLastShooterIndex();
            Assert.That(team.LastShooterIndex == 0);
        }
        
        private class TeamTestWrapper : Team
        {
            public TeamTestWrapper(List<Player> players) : base(players)
            {
                
            }
            
            public int LastShooterIndex => _lastShooterIndex;
            public void UpdateLastShooterIndex()
            {
                base.UpdateLastShooterIndex();
            }
        }
    }
}