using System.Data.Entity;
using PoolScoreboard.Application.DataAccess.Shot;
using PoolScoreboard.Application.DataAccess.User;

namespace PoolScoreboard.Application.DataAccess.AppContext
{
    class ScoreboardDatabase : DbContext
    {
        //private readonly string _schema;

        public ScoreboardDatabase()
            : base("name=ScoreboardConn")
        {
        }

        public DbSet<ShotResultDto> ShotResults { get; set; }
        public DbSet<PlayerDto> Players { get; set; }

        /*protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.HasDefaultSchema(_schema);
            base.OnModelCreating(builder);
        }*/
    }
}