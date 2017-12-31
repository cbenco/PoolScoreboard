using System.Runtime.InteropServices;

namespace PoolScoreboard.Application.Interfaces
{
    public interface IPlayerFactory
    {
        Player Create(string name);
    }

    public class PlayerFactory : IPlayerFactory
    {
        private static int _maxID = 1;
        public Player Create(string name)
        {
            return new Player
            {
                Name = name,
                Id = _maxID++
            };
        }
    }
}