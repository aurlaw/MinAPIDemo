using MinAPIDemo.Common;
using MinAPIDemo.Models;

var builder = WebApplication.CreateBuilder(args);
// configure services
builder.Services.AddEndpointDefinitions(typeof(Product));

var app = builder.Build();
// configure
app.UseEndpointDefinitions();


app.MapGet("/", () => "Hello World!");

app.Run();
