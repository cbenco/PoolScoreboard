using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PoolScoreboard.Application;

namespace PoolScoreboard.Tests
{
    public class EightBallRuleTests
    {
        [Test]
        [TestCase(new string[0], ShotResultType.Missed)]                    //clean break
        [TestCase(new[] {"4"}, ShotResultType.LegalPot)]                    //clean break, sunk a solid
        [TestCase(new[] {"12"}, ShotResultType.LegalPot)]                   //clean break, sunk a stripe
        [TestCase(new[] {"CUE"}, ShotResultType.WentInOff)]                 //Sunk the white
        [TestCase(new[] {"CUE", "4"}, ShotResultType.WentInOff)]            //Sunk the white and a solid
        [TestCase(new[] {"4", "12"}, ShotResultType.SunkBothOnBreak)]       //Sunk one solid and one stripe
        [TestCase(new[] {"8"}, ShotResultType.Win)]                         //Sunk the 8
        [TestCase(new[] {"8", "CUE"}, ShotResultType.Loss)]                 //Sunk the 8 and the white
        public void test_result_types_on_break(IEnumerable<string> ballsSunk, ShotResultType expected)
        {
            var result = SetUpShotResult("1", ballsSunk);

            Assert.AreEqual(result.Type, expected);
        }
        
        [Test]
        [TestCase("1", new[] {"1"}, BallClass.Solids, ShotResultType.LegalPot)]
        [TestCase("1", new[] {"4"}, BallClass.Solids, ShotResultType.LegalPot)]
        [TestCase("9", new string[]{}, BallClass.Solids, ShotResultType.WrongObjectBall)]
        [TestCase("9", new[] {"9"}, BallClass.Solids, ShotResultType.WrongObjectBall)]
        [TestCase("1", new[] {"9"}, BallClass.Solids, ShotResultType.SunkOpponentsBall)]
        [TestCase("9", new[] {"9"}, BallClass.Stripes, ShotResultType.LegalPot)]
        [TestCase("9", new[] {"15"}, BallClass.Stripes, ShotResultType.LegalPot)]
        [TestCase("9", new[] {"7"}, BallClass.Stripes, ShotResultType.SunkOpponentsBall)]
        public void test_object_ball_rules(string objectBall, IEnumerable<string> ballsSunk, BallClass ballClass, 
            ShotResultType expected)
        {
            var result = SetUpShotResult(objectBall, ballsSunk, ballClass, false);
            
            Assert.AreEqual(result.Type, expected);
        }
        
        private ShotResult SetUpShotResult(string objectBall, IEnumerable<string> sunk, BallClass shooting = BallClass.Neither, bool isBreak = true)
        {
            var rack = new EightBallRackTestWrapper();
            if (!isBreak)
                rack.SinkBall("2", true);
            var team1 = SetUpTeam(1);
            var team2 = SetUpTeam(1);
            team1.Shooting = shooting;
            team2.Shooting = shooting == BallClass.Neither ? BallClass.Neither : team1.Opposite;
            var shotResultFactory = new ShotResultFactory();
            return shotResultFactory.Create(Game.EightBallPool, team1, rack, objectBall, sunk);
        }

        private Team SetUpTeam(int numberOfPlayers)
        {
            List<Player> players = new List<Player>();
            for (int i = 0; i < numberOfPlayers; i++)
            {
                players.Add(new Player());
            }
            return new EightBallPoolTeam(players);
        }
    }
}