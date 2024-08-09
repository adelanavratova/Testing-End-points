using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();

var sampleTodos = new Todo[] {
    new(1, "Walk the dog", "Outside", "Friday"),
    new(2, "Do the dishes", "Kitchen", "Monday"),
    new(3, "Do the laundry", "Bathroom", "Tuesday"),
    new(4, "Clean the bathroom", "Bathroom", "Monday"),
    new(5, "Clean the car", "Outside", "Friday"),
    new(6, "Cook lunch", "Kitchen", "Friday"),
    new(7, "Rest", "Bedroom", "Monday")
};

//vytvoreni API
var todosApi = app.MapGroup("/todos");

//End-point 1: all
todosApi.MapGet("/", () => sampleTodos);

//End-point 2: Id
todosApi.MapGet("/id/{id}", (int id) => 
    sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
        ? Results.Ok(todo)
        : Results.NotFound());

//End-point 3: Day
todosApi.MapGet("/day/{day}", (string day) =>
{
    var todos = sampleTodos.Where(t => t.FinalDate.Equals(day, StringComparison.OrdinalIgnoreCase)).ToArray();
    return todos.Any() ? Results.Ok(todos) : Results.NotFound($"No todos found for day {day}.");
});

//End-point 4: Location
todosApi.MapGet("/location/{location}", (string location) =>
{
    var todos = sampleTodos.Where(t => t.Room.Equals(location, StringComparison.OrdinalIgnoreCase)).ToArray();
    return todos.Any() ? Results.Ok(todos) : Results.NotFound($"No todos found for location {location}.");
});

//End-point 5: Count
todosApi.MapGet("/count", () => sampleTodos.Length);

app.Run();

public record Todo(int Id, string Title, string Room, string FinalDate);

[JsonSerializable(typeof(Todo[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}
