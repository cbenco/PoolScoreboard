namespace PoolScoreboard.Application.DataAccess.Shot
{
    public class ShotResultDto
    {
        public int Id { get; set; }
        public int ShootingTeamId { get; set; } //the team
        public int Shooter { get; set; } //the player
        public string ObjectBall { get; set; }
        public string BallsSunkCsv { get; set; }
        public bool FirstLegalPot { get; set; }
        public ShotResultType Type { get; set; }
    }
}