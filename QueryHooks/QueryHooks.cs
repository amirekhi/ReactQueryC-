using ReactQuerySharp.Query;
using ReactQuerySharp.QueryObserver;
using ReactQuerySharp.QueryClientF;  // <-- ADD THIS

namespace ReactQuerySharp.QueryHooks
{
       public static class QueryHooks
    {
        public static QueryResult<T> UseQuery<T>(
            string key,
            Func<Task<T>> fetchFn,
            Action<Query<T>> observerCallback = null) // optional
        {
            var client = QueryClient.Instance;

            // Step 1: get or create query
            var query = client.GetQuery(key, fetchFn);

            // Step 2: subscribe observer if provided
            if (observerCallback != null)
            {
                var observer = new QueryObserver<T>(query, observerCallback);
            }

            // Step 3: trigger fetch if idle
            if (query.Status == QueryStatus.Idle)
            {
                _ = query.Fetch();
            }

            // Step 4: return current snapshot
            return new QueryResult<T>(query.Data, query.Status, query.Error);
        }
    }

}
