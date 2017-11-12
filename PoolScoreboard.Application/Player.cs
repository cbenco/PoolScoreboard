using System.Collections.Generic;
using System.Linq;

namespace PoolScoreboard.Application
{
    public interface IPlayer
    {
        int Id { get; set; }
        string Name { get; set; }
    }
    
    public class Player : IPlayer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}