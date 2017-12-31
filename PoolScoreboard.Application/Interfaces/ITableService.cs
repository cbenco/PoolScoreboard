using System.Collections.Generic;
using PoolScoreboard.Application.DataAccess.Shot;

namespace PoolScoreboard.Application.Interfaces
{
    public interface ITableService
    {
        ShotResult TakeShot(int tableId, string objectBall, IEnumerable<string> sunk);
    }
    
    public class TableService : ITableService
    {
        private IShotResultRepository _shotResultRepository = new ShotResultRepository();

        public ShotResult TakeShot(int tableId, string objectBall, IEnumerable<string> sunk)
        {
            return new ShotResult();
        }
    }
}