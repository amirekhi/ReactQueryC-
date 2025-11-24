using ReactQuerySharp.QueryClient;
using ReactQuerySharp.QueryObserver;


// the context holding queries creating them if needed
var client = new QueryClient();

var query = client.GetQuery(
    "todos",
    async () =>
    {
        await Task.Delay(1000);
        return new[] { "Todo 1", "Todo 2" };
    }
);

var observer = new QueryObserver<string[]>(
    query,
    (q) =>
    {
        Console.WriteLine($"Status: {q.Status}");
        if (q.Data != null)
            Console.WriteLine("Data: " + string.Join(", ", q.Data));
    }
);

await query.Fetch();
