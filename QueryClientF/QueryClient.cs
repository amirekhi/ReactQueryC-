using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactQuerySharp.Query;

namespace ReactQuerySharp.QueryClientF
{
        public sealed class QueryClient
        {
            private static readonly Lazy<QueryClient> _instance =
                new Lazy<QueryClient>(() => new QueryClient());

            public static QueryClient Instance => _instance.Value;

            private readonly Dictionary<string, object> _queries = new();

            private QueryClient()
            {
                // private constructor prevents outside instantiation
            }

            public Query<T> GetQuery<T>(string key, Func<Task<T>> fetchFn)
            {
                if (_queries.TryGetValue(key, out var existing))
                    return (Query<T>)existing;

                var query = new Query<T>(key, fetchFn);
                _queries[key] = query;
                return query;
            }
        }


}