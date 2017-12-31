using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PoolScoreboard.Application.DataAccess.AppContext;
using PoolScoreboard.Application.Interfaces;

namespace PoolScoreboard.Application.DataAccess.User
{
    public interface IPlayerRepository : IRepository<Player>
    {
        
    }
    
    public class PlayerRepository : IPlayerRepository
    {
        public Player Fetch(int id)
        {
            using (var db = new ScoreboardDatabase())
            {
                
            }
            return new Player();
        }
        
        public List<Player> List(Expression<Func<Player, bool>> whereCondition)
        {
            return new List<Player>();
        }

        public List<Player> List()
        {
            return new List<Player>();
        }
        
        public Player Save(Player player)
        {
            return new Player();
        }
        
        public void Delete(Player player)
        {
            Delete(player.Id);
        }

        public void Delete(int id)
        {
            
        }
    }
}