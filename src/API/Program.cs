using FacilitiesCoordinator.Features.HealthCheck;
using FacilitiesCoordinator.Features.Root;
using FacilitiesCoordinator.API.Features.Facilities.Create;
using FacilitiesCoordinator.API.Features.Facilities.Read;
using FacilitiesCoordinator.API.Features.FacilityGroup.Create;
using FacilitiesCoordinator.API.Features.FacilityGroup.Update;
using FacilitiesCoordinator.API.Features.FacilityGroup.GetAll;
using FacilitiesCoordinator.API.Features.FacilityGroup.GetById;
using FacilitiesCoordinator.API.Features.FacilityGroup.Delete;
using FacilitiesCoordinator.API.Features.FacilityGroup.GetAllFacilitiesByGroup;
using FacilitiesCoordinator.API.Features.Users.Create;
using FacilitiesCoordinator.API.Features.Users.GetAll;
using FacilitiesCoordinator.API.Features.Users.GetById;
using FacilitiesCoordinator.API.Features.Users.Update;

using System.Reflection;
using FacilitiesCoordinator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<CreateFacilityHandler>();
builder.Services.AddScoped<ReadFacilitiesHandler>();
builder.Services.AddScoped<ReadSingleFacilityHandler>();
builder.Services.AddScoped<CreateFacilityGroupHandler>();
builder.Services.AddScoped<UpdateFacilityGroupHandler>();
builder.Services.AddScoped<GetAllFacilityGroupsHandler>();
builder.Services.AddScoped<GetByIdFacilityGroupHandler>();
builder.Services.AddScoped<DeleteFacilityGroupHandler>();
builder.Services.AddScoped<GetAllFacilitiesByGroupHandler>();
builder.Services.AddScoped<CreateUserHandler>();
builder.Services.AddScoped<GetAllUsersHandler>();
builder.Services.AddScoped<GetUserByIdHandler>();
builder.Services.AddScoped<UpdateUserHandler>();
builder.Services.AddValidatorsFromAssemblyContaining<ReadSingleFacilityRequestValidator>();

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

//check the database connection at startup and exit if it fails
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    try
    {
        if (!await db.Database.CanConnectAsync())
        {
            app.Logger.LogCritical("Database connection failed");
            Environment.Exit(1);
        }

        app.Logger.LogInformation("Database connection OK");
    }
    catch (Exception ex)
    {
        app.Logger.LogCritical(ex, "Database connection failed");
        Environment.Exit(1);
    }
}

//add logging middleware to log incoming requests and outgoing responses
app.Use(async (context, next) =>
{
    var logger = context.RequestServices
        .GetRequiredService<ILoggerFactory>()
        .CreateLogger("HTTP");

    var start = Stopwatch.StartNew();

    logger.LogDebug("Incoming {Method} {Path}",
        context.Request.Method,
        context.Request.Path);

    await next();

    start.Stop();

    logger.LogDebug("Outgoing {StatusCode} in {Elapsed}ms",
        context.Response.StatusCode,
        start.ElapsedMilliseconds);
});


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();        
    app.UseSwaggerUI();      
}

// Map endpoints
app.MapHealthCheck();
app.MapRootEndpoint();
app.MapCreateFacilityEndpoint();
app.MapReadFacilitiesEndpoint();
app.MapReadSingleFacilityEndpoint();
app.MapCreateFacilityGroupEndpoint();
app.MapUpdateFacilityGroupEndpoint();
app.MapGetAllFacilityGroupsEndpoint();
app.MapGetByIdFacilityGroupEndpoint();
app.MapDeleteFacilityGroupEndpoint();
app.MapGetAllFacilitiesByGroupEndpoint();
app.MapCreateUserEndpoint();
app.MapGetAllUsersEndpoint();
app.MapGetUserByIdEndpoint();
app.MapUpdateUserEndpoint();

app.Run();
