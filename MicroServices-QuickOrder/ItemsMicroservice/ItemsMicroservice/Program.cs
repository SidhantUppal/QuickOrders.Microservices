// Program.cs
// Entry point and configuration for the ItemsMicroservice ASP.NET Core application

using ItemsMicroservice.ApplicationLayer.RepositoriesContract; // Interface for item repository
using ItemsMicroservice.ApplicationLayer.Services; // Implementation of item service
using ItemsMicroservice.ApplicationLayer.ServicesContract; // Interface for item service
using ItemsMicroservice.DataAccessLayer.Repositories; // Implementation of item repository
using Microsoft.EntityFrameworkCore; // Entity Framework Core namespace

namespace ItemsMicroservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args); // Create a new WebApplication builder

            // Add services to the container.

            builder.Services.AddControllers(); // Register controllers for API endpoints
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer(); // Register endpoint API explorer for Swagger
            builder.Services.AddSwaggerGen(); // Register Swagger generator for API documentation
            builder.Services.AddScoped<IItemsService, ItemsService>(); // Register ItemsService for dependency injection
            builder.Services.AddScoped<IItemsRepo, ItemsRepo>(); // Register ItemsRepo for dependency injection
            builder.Services.AddDbContext<DataAccessLayer.EntityFrameworkCore.DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("dbcs"))); // Register DataContext with SQL Server connection

            builder.Services.AddCors(options => // Register CORS policy
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin() // Allow requests from any origin
                          .AllowAnyHeader() // Allow any HTTP header
                          .AllowAnyMethod(); // Allow any HTTP method
                });
            });

            var app = builder.Build(); // Build the WebApplication

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) // If in development environment
            {
                app.UseSwagger(); // Enable Swagger middleware
                app.UseSwaggerUI(); // Enable Swagger UI
            }

            app.UseCors(); // Enable CORS middleware
            app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS

            app.UseAuthorization(); // Enable authorization middleware

            app.MapControllers(); // Map controller routes

            app.Run(); // Run the application
        }
    }
}
