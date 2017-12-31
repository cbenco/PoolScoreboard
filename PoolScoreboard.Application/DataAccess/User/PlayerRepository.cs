using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using PoolScoreboard.Application.DataAccess.AppContext;
using PoolScoreboard.Application.Interfaces;

namespace PoolScoreboard.Application.DataAccess.User
{
    public interface IPlayerRepository : IRepository<Player>
    {
        
    }
    
    public class PlayerRepository : Repository<Player, PlayerDto>, IPlayerRepository
    {
        public override Player Save(Player item)
        {
            var dto = new PlayerDto(item);
            
            using (var db = new ScoreboardDatabase())
            {
                var dbSet = db.GetDbSet(dto);
                
                if (!dto.Id.HasValue)
                {
                    dbSet.Add(dto);
                }
                //Check for player of same name
                var playerFromDb = dbSet.FirstOrDefault(p => p.Name == dto.Name);
                if (playerFromDb != null)
                {
                    item = CastFromDto(playerFromDb);
                }
                else
                {
                    db.SaveChanges();
                    item.Id = dto.Id;
                }
            }
            return item;
        }
        
        protected override Player CastFromDto(PlayerDto dto)
        {
            var result = new Player
            {
                Id = dto.Id,
                Name = dto.Name
            };
            return result;
        }
    }
}