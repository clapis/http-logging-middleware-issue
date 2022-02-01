using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpLogging(opts => opts.LoggingFields = HttpLoggingFields.RequestBody);

var app = builder.Build();

app.UseHttpLogging();

// OK - request body is logged as expected
app.MapPost("/hello", (Person person) => $"Hello {person.Name}!");

// Request body is not logged if body is not used
// curl --location --request POST 'http://localhost:5000/hello' \
// --header 'Content-Type: application/json' \
// --data-raw '{
// "name": "Bob"
// }'
app.MapPost("/bye", () => "Bye!");

// Unmapped requests also don't log the request body
// curl --location --request POST 'http://localhost:5000/things' \
// --header 'Content-Type: application/json' \
// --data-raw '{
// "id": 123
// }'

app.Run();

class Person
{
    public string Name { get; set; } = "Stranger";
}