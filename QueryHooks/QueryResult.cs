using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactQuerySharp.Query;

namespace ReactQuerySharp.QueryHooks
{
  public class QueryResult<T>
    {
        public T Data { get; private set; }
        public QueryStatus Status { get; private set; }
        public Exception Error { get; private set; }

        public QueryResult(T data = default, QueryStatus status = QueryStatus.Idle, Exception error = null)
        {
            Data = data;
            Status = status;
            Error = error;
        }

        internal void Update(Query<T> query)
        {
            Data = query.Data;
            Status = query.Status;
            Error = query.Error;
        }
    }
}