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


// lets say we are in another component 


var comp_two_query = client.GetQuery("todos",
    async () =>
    {
        await Task.Delay(1000);
        return new[] { "Todo 1", "Todo 2" };
    });

    QueryObserver<string[]> comp_two_observer = new QueryObserver<string[]>(comp_two_query, (q) =>
    {
          Console.WriteLine($"Status: {q.Status}");
        if (q.Data != null)
            Console.WriteLine("comp_two_Data: " + string.Join(", ", q.Data));
    });


   var queryData = await client.FetchQuery<string[]>("todos",
     async () =>
    {
        await Task.Delay(1000);
        return new[] { "Todo 1", "Todo 2" };
    });

    Console.WriteLine("queryData: " + string.Join(", ", queryData));