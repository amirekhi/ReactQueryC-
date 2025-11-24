using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactQuerySharp.Query;

namespace ReactQuerySharp.QueryHooks
{
    public class QueryResult<T>
    {
        public T Data { get; set; }
        public QueryStatus Status { get; set; }
        public Exception Error { get; set; }

        public QueryResult(T data, QueryStatus status, Exception error)
        {
            Data = data;
            Status = status;
            Error = error;
        }
    }
}