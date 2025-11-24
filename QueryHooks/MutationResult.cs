using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactQuerySharp.QueryHooks
{
    public class MutationResult<T>
    {
        public T Data { get; private set; }
        public Exception Error { get; private set; }
        public MutationStatus Status { get; private set; }

        public MutationResult()
        {
            Status = MutationStatus.Idle;
        }

        internal void Update(T data, MutationStatus status, Exception error = null)
        {
            Data = data;
            Status = status;
            Error = error;
            OnChanged?.Invoke(this);
        }

        public event Action<MutationResult<T>> OnChanged;
    }

    public enum MutationStatus
{
    Idle,
    Loading,
    Success,
    Error
}

}