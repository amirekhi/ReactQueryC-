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
            r => Console.WriteLine($"Observer Update: {r.Status} | {string.Join(", ", r.Data ?? Array.Empty<string>())}")
        );

        // Initial snapshot
        Console.WriteLine($"Initial Status: {result.Status}");

        // Wait to see async fetch finish
        await Task.Delay(1500);
       var result_2 = QueryHooks.UseQuery(
            "todos",
            async () =>
            {
                await Task.Delay(1000);
                return new[] { "Todo 1", "Todo 2" , "Todo 3" };
            },
            r => Console.WriteLine($"Observer Update: {r.Status} | {string.Join(", ", r.Data ?? Array.Empty<string>())}")
        );



        // result now has updated data automatically
        Console.WriteLine($"Final Data: {string.Join(", ", result.Data ?? Array.Empty<string>())}");

    }
}
