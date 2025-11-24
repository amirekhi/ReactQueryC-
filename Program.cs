using System;
using System.Threading.Tasks;
using ReactQuerySharp.QueryHooks;
using ReactQuerySharp.Query;

class Program
{
    static async Task Main(string[] args)
    {
        var result = QueryHooks.UseQuery(
            "todos",
            async () =>
            {
                await Task.Delay(1000);
                return new[] { "Todo 1", "Todo 2" };
            },
            q => Console.WriteLine($"Observer Update: {q.Status} | Data: {string.Join(", ", q.Data ?? Array.Empty<string>())}")
        );

        Console.WriteLine($"Initial Status: {result.Status}"); // Idle or Loading

        // Wait to see async fetch finish
        await Task.Delay(1500);

        var result2 = QueryHooks.UseQuery(
            "todos",
            async () => Array.Empty<string>() // same key, fetchFn ignored
        );

        Console.WriteLine($"After fetch: {string.Join(", ", result2.Data ?? Array.Empty<string>())}");
    }
}
