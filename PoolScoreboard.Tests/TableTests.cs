using System.Collections.Generic;
using NUnit.Framework;
using PoolScoreboard.Application;

namespace PoolScoreboard.Tests
{
    public class TableTests
    {
        [Test]
        [TestCase("1", new[] {"1"}, 1)]        //sink own ball, repeat turn
        [TestCase("1", new string[] {}, 2)]    //sink nothing, pass to other team
        [TestCase("9", new string[] {}, 2)]        //sink wrong ball, pass to other team
        public void test_turn_passing_rules(string objectBall, IEnumerable<string> ballsSunk, int expected)
        {
            var table = SetUpTable();
            table.PlayShot(objectBall, ballsSunk);

            Assert.That(Table.CurrentShooter.Id == expected);
        }

        private Table SetUpTable(bool isBreak = false)
        {
            var team1 = SetUpTeam(1, 1, BallClass.Solids);
            var team2 = SetUpTeam(1, 2, BallClass.Stripes);
            var rack = new EightBallRackTestWrapper();
            if (isBreak) rack.SinkBall("1", true);
            return new Table(rack, team1, team2);
        }

        private Team SetUpTeam(int numberOfPlayers, int id, BallClass shooting = BallClass.Neither)
        {
            List<Player> players = new List<Player>();
            for (int i = 0; i < numberOfPlayers; i++)
            {
                players.Add(new Player());
            }
            return new EightBallPoolTeam(players)
            {
                Id = id
            };
        }
    }
}