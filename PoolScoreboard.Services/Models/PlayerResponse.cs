using System.Collections.Generic;
using System.Linq;
using PoolScoreboard.Application;

namespace PoolScoreboard.Services.Models
{
    public class PlayerResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool CurrentShooter { get; set; }

        public PlayerResponse(IPlayer player, int currentShooterId)
        {
            Id = player.Id;
            Name = player.Name;
            CurrentShooter = Id == currentShooterId;
        }

        public static IEnumerable<PlayerResponse> CreateMany(IEnumerable<IPlayer> players, int currentShooterId)
        {
            return players.Select(p => new PlayerResponse(p, 0)).ToList();
        }
    }
}