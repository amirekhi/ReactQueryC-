using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactQuerySharp.Query;

namespace ReactQuerySharp.QueryObserver
{
    public class QueryObserver<T>
    {
        private readonly Query<T> _query;

        public QueryObserver(Query<T> query, Action<Query<T>> callback)
        {
            _query = query;
            _query.Subscribe(callback);
        }

        public void Unsubscribe(Action<Query<T>> callback)
        {
            _query.Unsubscribe(callback);
        }

        public Task Refetch() => _query.Fetch();
    }

}