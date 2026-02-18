using FacilitiesCoordinator.API.Endpoints;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

builder.Services.AddSwaggerGen(c =>
{
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();        // Serves the Swagger JSON
    app.UseSwaggerUI();      // Serves the Swagger UI
}

// Map endpoints
app.MapHealthCheck();
app.MapRootEndpoint();

app.Run();
