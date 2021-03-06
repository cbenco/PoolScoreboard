﻿using System.Collections.Generic;
using System.Linq;
using PoolScoreboard.Common;

namespace PoolScoreboard.Application
{
    public class Rules
    {
        
    }
    
    public class EightBallPoolRules : Rules
    {
        public static ShotResultType ValidateShot(EightBallPoolRack rack, ShotResult result)
        {
            var ballsSunk = result.BallsSunk.ToList();
            var objectBall = result.ObjectBall;
            var team = result.ShootingTeam;

            if (MissedObjectBall(objectBall))
                if (rack.HasNoColours)
                    return ShotResultType.Loss;
                else return ShotResultType.MissedObjectBall;
            
            if (WrongObjectBall(rack, objectBall, team))
                return ShotResultType.WrongObjectBall; //Hit the opponent's ball first
            
            if (SunkNone(ballsSunk))
                return ShotResultType.Missed;

            if (SunkBlack(ballsSunk))
            {
                if (HasBallsOnTable(rack, team) || 
                    WentInOff(ballsSunk) || 
                    SunkOpponentBall(ballsSunk, team))
                    return ShotResultType.Loss; //Sunk 8 ball early or sunk opponent's ball
                if (!HasBallsOnTable(rack, team)) //Sunk the 8 legally (or :O on the break)
                    return ShotResultType.Win;
            }
            
            if (WentInOff(ballsSunk)) 
                if (rack.HasNoColours)
                    return ShotResultType.Loss;       //
                else return ShotResultType.WentInOff; //Sunk the cue ball

            if (SunkOpponentBall(ballsSunk, team))
                return ShotResultType.SunkOpponentsBall; //Sunk the opponent's balls, turn ends

            if (SunkBothTypes(ballsSunk))
                return ShotResultType.SunkBothOnBreak;    //Sunk one of each on the break, turn continues
            
            return ShotResultType.LegalPot;
        }
        
        private static bool WrongObjectBall(EightBallPoolRack rack, IBall objectBall, ITeam shooter)
        {
            if (objectBall?.Identifier == "8" && !HasBallsOnTable(rack, shooter))
                return false;
            return !rack.OpenTable && objectBall.Class != shooter.Class;
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
        
        private static bool HasBallsOnTable(EightBallPoolRack rack, ITeam team)
        {
            return team.Class != BallClass.EightBall && rack.Any(x => x.Class == team.Class && x.OnTable);
        }
        
        private static bool MissedObjectBall(IBall objectBall)
        {
            return objectBall == null;
        }
    }
}