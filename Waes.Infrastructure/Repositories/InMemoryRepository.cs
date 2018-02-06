using System.Collections.Concurrent;
using Waes.Core.Interfaces;

namespace Waes.Infrastructure.Repositories
{
    public class InMemoryRepository : IInMemoryRepository
    {
        private static readonly ConcurrentDictionary<string, string> _diffResults = new ConcurrentDictionary<string, string>();
        public string GetByKey(string key)
        {
            return _diffResults.ContainsKey(key) ? _diffResults[key] : string.Empty;
        }

        public void Save(string key, string value)
        {
            _diffResults.AddOrUpdate(key, value, (k, oldValue) => value);
        }

        public void Delete(string key)
        {
            string removedValue;
            _diffResults.TryRemove(key, out removedValue);
            removedValue = null;
        }
    }
}
