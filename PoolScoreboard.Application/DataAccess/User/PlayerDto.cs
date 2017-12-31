using PoolScoreboard.Application.DataAccess.AppContext;

namespace PoolScoreboard.Application.DataAccess.User
{
    public class PlayerDto : EntityDto
    {
        public string Name { get; set; }

        public PlayerDto(IPlayer player)
        {
            Name = player.Name;
        }
    }
}