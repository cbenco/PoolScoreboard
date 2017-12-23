using System.Collections.Generic;
using System.Linq;

namespace PoolScoreboard.Application
{
    public class ShotResult
    {
        public int? Id { get; set; }
        
        public ITeam ShootingTeam { get; set; }
        public IPlayer Shooter { get; set; }
        public IBall ObjectBall { get; set; }
        public IEnumerable<IBall> BallsSunk { get; set; }
        public bool LegalPot => Type == ShotResultType.LegalPot || Type == ShotResultType.Win;
        public bool FirstLegalPot { get; set; }
        public ShotResultType Type { get; set; }
        
        public override string ToString()
        {
            var result = "";
            result += $"Shot by {Shooter.Name}";
            result += $"\nLegal | ";
            result = AppendList(result, BallsSunk.Where(b => b.LegallySunk));
            result += $"\nIllegal | ";
            result = AppendList(result, BallsSunk.Where(b => !b.LegallySunk));
            result += $"\nShot result: {Type.ToString()}";
            return result;
        }
        
        private string AppendList(string result, IEnumerable<IBall> balls)
        {
            foreach (var ball in balls)
            {
                result += $"{ball.Identifier} ";
            }
            return result;
        }
        
        public string ConvertBallsSunkToCsv()
        {
            var ballsArray = BallsSunk.ToArray();
            if (!ballsArray.Any()) return string.Empty;

            var firstBall = ballsArray[0];
            var result = BallString(firstBall);
            for (var i = 1; i < ballsArray.Length - 1; i++)
            {
                var currentBall = ballsArray[i];
                result += $",{BallString(currentBall)}";
            }
            if (ballsArray.Length > 1)
            {
                result += "," + BallString(ballsArray[ballsArray.Length - 1]);
            }
            return result;
        }

        private string BallString(IBall ball)
        {
            return $"{ball.Identifier}|{GetLegallySunkChar(ball.LegallySunk)}";
        }

        private IEnumerable<IBall> ConvertFromCsv(Game game, string balls)
        {
            if (game == Game.EightBallPool)
            {
                return ConvertPoolBallsFromStrings(balls);
            }
            return new List<PoolBall>();
        }

        protected IEnumerable<Ball> ConvertPoolBallsFromStrings(string balls)
        {
            var result = new List<Ball>();
            foreach (var ball in balls.Split(','))
            {
                var strings = ball.Split('|');
                var id = strings[0];
                var legal = strings[1] == "t";
                if (id == "CUE")
                    result.Add(new CueBall());
                else
                    result.Add(new PoolBall(id, legal));
            }
            return result;
        }

        private string GetLegallySunkChar(bool legallySunk)
        {
            return legallySunk ? "t" : "f";
        }
    }
    
    public class PoolShotResult : ShotResult
    {
    }
    
    public enum ShotResultType
    {
        Missed,
        LegalPot,
        WrongObjectBall,
        WentInOff,
        SunkBothOnBreak,
        SunkOpponentsBall,
        MissedObjectBall,
        Win,
        Loss
    }
}