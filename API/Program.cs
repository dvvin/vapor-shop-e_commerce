using API.Extensions;
using API.Middleware;
using Core.Entities.Identity;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure the DbContext with SQL Lite.
builder.Services.AddDbContext<StoreContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Configure Identity
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("IdentityConnection"))
);

// Configure Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
{
    var configuration = ConfigurationOptions.Parse(
        builder.Configuration.GetConnectionString("Redis")!,
        true
    );
    return ConnectionMultiplexer.Connect(configuration);
});

builder.Services.AddApplicationServices(); // Import of ApplicationServicesExtensions.cs
builder.Services.AddSwaggerDocumentation(); // Import of SwaggerServiceExtensions.cs
builder.Services.AddIdentityServices(builder.Configuration); // Import of IdentityServiceExtensions.cs

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

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

        var userManager = services.GetRequiredService<UserManager<AppUser>>(); // Retrieves an instance of UserManager<AppUser> from the service provider.
        var identityContext = services.GetRequiredService<AppIdentityDbContext>(); // Retrieves an instance of AppIdentityDbContext from the service provider.
        await identityContext.Database.MigrateAsync(); // Applies any pending migrations for the identity context to the database. Creates the database if it does not already exist.
        await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
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

// app.UseHttpsRedirection();

app.UseStaticFiles(
    new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/browser")
        ),
        RequestPath = ""
    }
);

app.UseStaticFiles(
    new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "Content")
        ),
        RequestPath = "/content"
    }
);

app.UseRouting();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToController("Index", "Fallback");

app.Run();
