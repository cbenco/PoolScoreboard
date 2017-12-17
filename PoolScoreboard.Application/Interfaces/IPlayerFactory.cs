using System.Runtime.InteropServices;

namespace PoolScoreboard.Application.Interfaces
{
    public interface IPlayerFactory
    {
        IPlayer Create(string name);
    }

    public class PlayerFactory : IPlayerFactory
    {
        private static int _maxID = 1;
        public IPlayer Create(string name)
        {
            return new Player
            {
                Name = name,
                Id = _maxID++
            };
        }
    }
}