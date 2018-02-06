using System;
using StackExchange.Redis;
using System.Configuration;
using Waes.Core.Interfaces;

namespace Waes.Infrastructure.Repositories
{
    public class RedisInMemoryRepository : IInMemoryRepository
    {
        private static ConnectionMultiplexer _redisClient;
        private static ConnectionMultiplexer RedisClient =>
            _redisClient ?? (_redisClient = ConnectionMultiplexer.Connect(ConfigurationManager.AppSettings["CacheConnection"]));

        private IDatabase Db => RedisClient.GetDatabase();
        
        public string GetByKey(string key)
        {
            return Db.StringGet(key);
        }
        
        public void Save(string key, string value)
        {
            Db.StringSet(key, value, new TimeSpan(1, 0, 0, 0));
        }

        public void Delete(string key)
        {
            Db.KeyDelete(key);
        }
    }
}
