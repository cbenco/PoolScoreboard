using System;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using PoolScoreboard.Application.DataAccess.Match;
using PoolScoreboard.Application.DataAccess.Shot;
using PoolScoreboard.Application.DataAccess.Team;
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
        
        public DbSet<TeamDto> Teams { get; set; }
        public DbSet<FrameDto> Frames { get; set; }
        public DbSet<ShotResultDto> ShotResults { get; set; }
        public DbSet<PlayerDto> Players { get; set; }

        public IDbSet<T> GetDbSet<T>(T dto) where T : EntityDto
        {
            return Set<T>();
        }

        /*protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.HasDefaultSchema(_schema);
            base.OnModelCreating(builder);
        }*/
    }
}