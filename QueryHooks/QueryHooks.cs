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
            Action<QueryResult<T>> callback = null) // optional
        {
            var client = QueryClient.Instance;

            // 1. get or create query
            var query = client.GetQuery(key, fetchFn);

            // 2. create a QueryResult<T> snapshot
            var result = new QueryResult<T>(query.Data, query.Status, query.Error);

            // 3. internal callback to update result whenever query changes
            void InternalCallback(Query<T> q)
            {
                result.Update(q);           // update the snapshot
                callback?.Invoke(result);   // call user-provided callback if any
            }

            // 4. subscribe internal callback to query
            var observer = new QueryObserver<T>(query, InternalCallback);

            // 5. trigger fetch if idle
            if (query.Status == QueryStatus.Idle)
            {
                _ = query.Fetch();
            }

            // 6. return the snapshot
            return result;
        }
    }

}
