namespace PoolScoreboard.Application.DataAccess.Shot
{
    public class ShotResultDto
    {
        public int? Id { get; set; }
        public int ShootingTeamId { get; set; } //the team
        public int Shooter { get; set; } //the player
        public string ObjectBall { get; set; }
        public string BallsSunkCsv { get; set; }
        public bool FirstLegalPot { get; set; }
        public string Type { get; set; }

        //I loathe having the constructor here, but what can you do
        public ShotResultDto() {}
        
        public ShotResultDto(ShotResult shotResult)
        {
            BallsSunkCsv = shotResult.ConvertBallsSunkToCsv();
            FirstLegalPot = shotResult.FirstLegalPot;
            Id = shotResult.Id;
            ObjectBall = shotResult.ObjectBall.Identifier;
            Shooter = shotResult.Shooter.Id;
            ShootingTeamId = shotResult.ShootingTeam.Id;
            Type = shotResult.Type.ToString();
        }
    }
}