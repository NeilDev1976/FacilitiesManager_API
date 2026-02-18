using FacilitiesCoordinator.API.Endpoints;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Map endpoints
app.MapHealthCheck();
app.MapRootEndpoint();

app.Run();
