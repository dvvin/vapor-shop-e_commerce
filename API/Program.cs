using API.Extensions;
using API.Middleware;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure the DbContext with SQL Lite.
builder.Services.AddDbContext<StoreContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddApplicationServices(); // Import of ApplicationServicesExtensions.cs
builder.Services.AddSwaggerDocumentation(); // Import of SwaggerServiceExtensions.cs

var app = builder.Build();

// Using block to create a scope for the service provider.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider; // Gets the service provider from the scope.
    var loggerFactory = services.GetRequiredService<ILoggerFactory>(); // Retrieves an instance of ILoggerFactory used for logging.

    try
    {
        var context = services.GetRequiredService<StoreContext>(); // Retrieves an instance of StoreContext from the service provider.
        await context.Database.MigrateAsync(); // Applies any pending migrations for the context to the database. Creates the database if it does not already exist.

        await StoreContextSeed.SeedAsync(context, loggerFactory); // Seeds the database with initial data.
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>(); // Creates a logger instance for the Program class.
        logger.LogError(ex, "An error occurred during migration"); // Logs the exception if there's an error during the migration.
    }
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseSwaggerDocumentation();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
