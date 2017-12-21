using System;
using PoolScoreboard.Application;
using PoolScoreboard.Services.Models;

namespace PoolScoreboard.Services.Factories
{
    public interface ITableResponseFactory
    {
        TableResponse Create(Game game, Table table);
    }

    public class TableResponseFactory : ITableResponseFactory
    {
        public TableResponse Create(Game game, Table table)
        {
            switch (game)
            {
                case Game.EightBallPool:
                    return new EightBallTableResponse(table);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}