using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactQuerySharp.QueryHooks;

namespace ReactQuerySharp.Mute
{
  public static class MutationHooks
    {
        public static Mutation<T> UseMutation<T>(
            Func<Task<T>> mutationFn,
            Action<MutationResult<T>> callback = null)
        {
            var mutation = new Mutation<T>(mutationFn);

            if (callback != null)
            {
                mutation.Result.OnChanged += callback;
            }

            return mutation;
        }
    }
}