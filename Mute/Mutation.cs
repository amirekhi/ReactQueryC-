using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactQuerySharp.QueryClientF;
using ReactQuerySharp.QueryHooks;

namespace ReactQuerySharp.Mute
{
  public class Mutation<T>
    {
        private readonly Func<Task<T>> _mutationFn;
        private readonly MutationResult<T> _result;

        public Mutation(Func<Task<T>> mutationFn)
        {
            _mutationFn = mutationFn;
            _result = new MutationResult<T>();
        }

        public MutationResult<T> Result => _result;

        public async Task ExecuteAsync(
            Action callbackOnSuccess = null,
            Action<QueryClient> invalidateQueries = null) // optional
        {
            if (_result.Status == MutationStatus.Loading)
                return;

            try
            {
                _result.Update(default, MutationStatus.Loading);

                var data = await _mutationFn();
                _result.Update(data, MutationStatus.Success);

                // call the regular success callback
                callbackOnSuccess?.Invoke();

                // notify other queries (invalidate)
                invalidateQueries?.Invoke(QueryClient.Instance);
            }
            catch (Exception ex)
            {
                _result.Update(default, MutationStatus.Error, ex);
            }
        }

    }
}