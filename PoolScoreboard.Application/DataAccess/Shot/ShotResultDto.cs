using PoolScoreboard.Application.DataAccess.AppContext;

namespace PoolScoreboard.Application.DataAccess.Shot
{
    public class ShotResultDto : EntityDto
    {
        public int FrameId { get; set; }
        public int? ShootingTeamId { get; set; } //the team
        public int Shooter { get; set; } //the player
        public string ObjectBall { get; set; }
        public string BallsSunkCsv { get; set; }
        public bool FirstLegalPot { get; set; }
        public string Type { get; set; }

        //I loathe having the constructor here, but what can you do
        public ShotResultDto() {}
        
        public ShotResultDto(ShotResult shotResult)
        {
            if (!shotResult.Frame.Id.HasValue) return;
            
            FrameId = shotResult.Frame.Id.Value;
            BallsSunkCsv = shotResult.ConvertBallsSunkToCsv();
            FirstLegalPot = shotResult.FirstLegalPot;
            Id = shotResult.Id;
            ObjectBall = shotResult.ObjectBall.Identifier;
            Shooter = shotResult.Shooter.Id ?? 0;
            ShootingTeamId = shotResult.ShootingTeam?.Id;
            Type = shotResult.Type.ToString();
        }
    }
}