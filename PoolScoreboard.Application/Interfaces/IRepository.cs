using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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
}