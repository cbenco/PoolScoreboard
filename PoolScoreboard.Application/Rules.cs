using System.Collections.Generic;
using System.Linq;
using PoolScoreboard.Common;

namespace PoolScoreboard.Application
{
    public class Rules
    {
        
    }
    
    public class EightBallPoolRules : Rules
    {
        public static ShotResultType ValidateShot(IRack rack, ShotResult result, bool isBreak)
        {
            var ballsSunk = result.BallsSunk.ToList();

            if (Scratch(result.ObjectBall))
                return ShotResultType.Scratch;
            
            if (WrongObjectBall((EightBallPoolRack) rack, result.ObjectBall, result.ShootingTeam))
                return ShotResultType.WrongObjectBall; //Hit the opponent's ball first
            
            if (SunkNone(ballsSunk))
                return ShotResultType.Missed;

            if (SunkBlack(ballsSunk))
            {
                if (HasBallsOnTable((EightBallPoolRack) rack, result.ShootingTeam) || WentInOff(ballsSunk))
                    return ShotResultType.Loss; //Sunk 8 ball early
                if (!HasBallsOnTable((EightBallPoolRack) rack, result.ShootingTeam)) //Sunk the 8 legally (or :O on the break)
                    return ShotResultType.Win;
            }
            
            if (WentInOff(ballsSunk)) 
                return ShotResultType.WentInOff; //Sunk the cue ball

            if (SunkOpponentBall(ballsSunk, result.ShootingTeam))
                return ShotResultType.SunkOpponentsBall; //Sunk the opponent's balls, turn ends

            if (SunkBothTypes(ballsSunk))
                return ShotResultType.SunkBothOnBreak;    //Sunk one of each on the break, turn continues
            
            return ShotResultType.LegalPot;
        }
        
        private static bool WrongObjectBall(EightBallPoolRack rack, IBall objectBall, ITeam shooter)
        {
            return !rack.OpenTable && objectBall.Class != shooter.Shooting;
        }
        
        private static bool WentInOff(ICollection<IBall> ballsSunk)
        {
            return ballsSunk.Contains(Table.CueBall);
        }

        private static bool SunkNone(ICollection<IBall> ballsSunk)
        {
            return !ballsSunk.Any();
        }

        private static bool SunkOpponentBall(IEnumerable<IBall> ballsSunk, ITeam shooter)
        {
            return ballsSunk.Any(b => b.Class == shooter.Opposite);
        }
        
        private static bool SunkBothTypes(ICollection<IBall> ballsSunk)
        {
            return ballsSunk.Any(b => b.Class == BallClass.Solids) &&
                   ballsSunk.Any(b => b.Class == BallClass.Stripes);
        }

        private static bool SunkBlack(ICollection<IBall> ballsSunk)
        {
            return ballsSunk.Any(b => b.Identifier == "8");
        }

        private static bool WonOnBreak(ICollection<IBall> ballsSunk, EightBallPoolRack rack)
        {
            return SunkBlack(ballsSunk) && rack.IsBreak;
        }
        
        private static bool HasBallsOnTable(EightBallPoolRack rack, ITeam team)
        {
            return rack.Any(x => x.Class == team.Shooting && !x.OnTable);
        }
        
        private static bool Scratch(IBall objectBall)
        {
            return objectBall == null;
        }
    }
}