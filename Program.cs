using System;
using System.Linq;
using System.Threading.Tasks;
using ReactQuerySharp.QueryHooks;
using ReactQuerySharp.QueryClientF;
using ReactQuerySharp.Mute;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("=== ReactQuerySharp Demo ===");

        // Simulated data store
        var todosStore = new[] { "Todo 1", "Todo 2" }.ToList();

        // -------------------------
        // Step 1: Create a query
        // -------------------------
        var todosQuery = QueryHooks.UseQuery(
            "todos",
            async () =>
            {
                await Task.Delay(500); // simulate network
                return todosStore.ToArray();
            },
            r => Console.WriteLine($"Query Observer: {r.Status} | {string.Join(", ", r.Data ?? Array.Empty<string>())}")
        );

        // Initial snapshot
        Console.WriteLine($"Initial query status: {todosQuery.Status}");

        await Task.Delay(600); // wait for first fetch to complete

        // -------------------------
        // Step 2: Create a mutation
        // -------------------------
        var addTodoMutation = MutationHooks.UseMutation(
            async () =>
            {
                await Task.Delay(500); // simulate network
                var newTodo = $"Todo {todosStore.Count + 1}";
                todosStore.Add(newTodo);
                Console.WriteLine($"Mutation executed: added {newTodo}");
                return newTodo;
            }
        );

        // Step 3: Execute mutation and invalidate query
        await addTodoMutation.ExecuteAsync(
            callbackOnSuccess: () => Console.WriteLine("Mutation success callback triggered!"),
            invalidateQueries: client =>
            {
                Console.WriteLine("Invalidating 'todos' query...");
                client.InvalidateQuery("todos");
            }
        );

        await Task.Delay(600); // wait for refetch to complete

        Console.WriteLine("Final todos list:");
        var finalTodos = QueryClient.Instance.GetQuery<string[]>("todos", null).Data as string[];
        Console.WriteLine(string.Join(", ", finalTodos ?? Array.Empty<string>()));
    }
}
