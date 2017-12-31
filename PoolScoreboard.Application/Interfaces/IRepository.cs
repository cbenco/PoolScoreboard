using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using PoolScoreboard.Application.DataAccess.AppContext;

namespace PoolScoreboard.Application.Interfaces
{
    public interface IRepository<T>
    {
        T Fetch(int id);
        T Save(T itemToSave);
        void Delete(T itemToUpdate);
        void Delete(int id);
        List<T> List(Expression<Func<T, bool>> whereCondition);
        List<T> List();
    }

    public abstract class Repository<T, TDto> : IRepository<T> where T : ISaveable, new() where TDto : EntityDto
    {
        private readonly IEntityDtoFactory _entityDtoFactory = new EntityDtoFactory();

        public T Fetch(int id)
        {
            var dto = (TDto)_entityDtoFactory.Create(new T());

            using (var db = new ScoreboardDatabase())
            {
                var dbSet = db.GetDbSet(dto);
                dto = dbSet.FirstOrDefault(x => x.Id == id);

                return CastFromDto(dto);
            }
        }

        protected abstract T CastFromDto(TDto dto);

        public List<T> List(Expression<Func<T, bool>> whereCondition)
        {
            throw new NotImplementedException();
        }

        public List<T> List()
        {
            throw new NotImplementedException();
        }

        public T Save(T item)
        {
            var dto = (TDto)_entityDtoFactory.Create(item);
            
            using (var db = new ScoreboardDatabase())
            {
                var dbSet = db.GetDbSet(dto);
                
                if (!dto.Id.HasValue)
                {
                    dbSet.Add(dto);
                }
                else
                {
                    dbSet.Attach(dto);
                    db.Entry(dto).State = EntityState.Modified;
                }
                db.SaveChanges();
                item.Id = dto.Id;
            }
            return item;
        }

        public void Delete(T item)
        {
            if (item.Id.HasValue)
                Delete(item.Id.Value);
        }
        
        public void Delete(int id)
        {

        }
    }
}