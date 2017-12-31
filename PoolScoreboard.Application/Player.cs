using PoolScoreboard.Application.Interfaces;

namespace PoolScoreboard.Application
{
    public interface IPlayer
    {
        int? Id { get; set; }
        string Name { get; set; }
    }
    
    public class Player : ISaveable, IPlayer
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"  - {Id}: {Name}";
        }
    }
}