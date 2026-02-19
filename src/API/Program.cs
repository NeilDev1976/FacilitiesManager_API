using FacilitiesCoordinator.API.Endpoints;
using System.Reflection;
using FacilitiesCoordinator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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

var connectionString = Environment.GetEnvironmentVariable("FACILITY_COORDINATOR_DB_CONNECTION")
                       ?? builder.Configuration.GetConnectionString("FacilityCoordinatorDb");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

app.MapGet("/facilities", async (AppDbContext db) => await db.Facilities.ToListAsync());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();        // Serves the Swagger JSON
    app.UseSwaggerUI();      // Serves the Swagger UI
}

// Map endpoints
app.MapHealthCheck();
app.MapRootEndpoint();

app.Run();
