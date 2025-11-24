using ReactQuerySharp.Query;
using ReactQuerySharp.QueryObserver;
using ReactQuerySharp.QueryClientF;  // <-- ADD THIS

namespace ReactQuerySharp.QueryHooks
{
    public static class QueryHooks
    {
        public static Query<T> UseQuery<T>(
            string key,
            Func<Task<T>> fetchFn,
            Action<Query<T>> observerCallback)
        {
            var client = QueryClient.Instance;

            var query = client.GetQuery(key, fetchFn);

            var observer = new QueryObserver<T>(query, observerCallback);

            if (query.Status == QueryStatus.Idle)
            {
                _ = query.Fetch(); 
            }

            return query;
        }
    }
}
