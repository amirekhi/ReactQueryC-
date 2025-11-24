using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactQuerySharp.Query
{
    public class Query<T>
    {
        public string Key { get; }
        public T Data { get; private set; }
        public Exception Error { get; private set; }
        public QueryStatus Status { get; private set; }

        private readonly List<Action<Query<T>>> _observers = new();
        public Func<Task<T>> FetchFn { get; }

        public Query(string key, Func<Task<T>> fetchFn)
        {
            Key = key;
            FetchFn = fetchFn;
            Status = QueryStatus.Idle;
        }

        public void Subscribe(Action<Query<T>> observer)
        {
            _observers.Add(observer);
            observer(this); // immediately notify current state
        }

        public void Unsubscribe(Action<Query<T>> observer)
        {
            _observers.Remove(observer);
        }

        public async Task Fetch()
        {
            if (Status == QueryStatus.Loading) return;

            try
            {
                Status = QueryStatus.Loading;
                Notify();

                Data = await FetchFn();
                Status = QueryStatus.Success;

                Notify();
            }
            catch (Exception ex)
            {
                Error = ex;
                Status = QueryStatus.Error;
                Notify();
            }
        }

        private void Notify()
        {
            foreach (var observer in _observers)
                observer(this);
        }
    }
    public enum QueryStatus
    {
        Idle,
        Loading,
        Success,
        Error
    }

}
