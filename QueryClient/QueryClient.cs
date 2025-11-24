using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactQuerySharp.Query;

namespace ReactQuerySharp.QueryClient
{
    public class QueryClient
    {
        private readonly Dictionary<string, object> _queries = new();

        //getting the query itself
        public Query<T> GetQuery<T>(string key, Func<Task<T>> fetchFn)
        {
            if (_queries.TryGetValue(key, out var existing))
            {
                return (Query<T>)existing;
            }

            var query = new Query<T>(key, fetchFn);
            _queries[key] = query;

            return query;
        }
        //getting the data through query
        public async Task<T> FetchQuery<T>(string key, Func<Task<T>> fetchFn)
        {
            var q = GetQuery(key, fetchFn);
            await q.Fetch();
            return q.Data;
        }
    }

}