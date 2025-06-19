using ItemsMicroservice.ApplicationLayer.RepositoriesContract; // Import repository contract interface
using ItemsMicroservice.ApplicationLayer.Services; // Import OrdersService implementation
using ItemsMicroservice.ApplicationLayer.ServicesContract; // Import service contract interface
using ItemsMicroservice.DataAccessLayer.EntityFrameworkCore; // Import EF Core data context
using ItemsMicroservice.DataAccessLayer.Repositories; // Import OrdersRepo implementation
using Microsoft.EntityFrameworkCore; // Import EF Core

namespace OrdersMicroservice // Namespace for the OrdersMicroservice application
{
    public class Program // Main program class
    {
        public static void Main(string[] args) // Application entry point
        {
            var builder = WebApplication.CreateBuilder(args); // Create a new WebApplication builder with command-line args

            // Add services to the container.

            builder.Services.AddControllers(); // Register controllers for dependency injection
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer(); // Register endpoint API explorer for Swagger
            builder.Services.AddSwaggerGen(); // Register Swagger generator for API documentation
            builder.Services.AddScoped<IOrdersService, OrdersService>(); // Register OrdersService for IOrdersService (scoped lifetime)
            builder.Services.AddScoped<IOrdersRepo, OrdersRepo>(); // Register OrdersRepo for IOrdersRepo (scoped lifetime)
            builder.Services.AddDbContext<DataContext>(options => // Register DataContext with SQL Server provider
                options.UseSqlServer(builder.Configuration.GetConnectionString("dbcs")) // Use connection string from configuration
            );

            builder.Services.AddCors(options => // Register CORS services
            {
                options.AddPolicy("AllowAll", builder => // Define CORS policy named "AllowAll"
                {
                    builder.AllowAnyOrigin() // Allow requests from any origin
                           .AllowAnyHeader() // Allow any HTTP header
                           .AllowAnyMethod(); // Allow any HTTP method
                });
            });

            var app = builder.Build(); // Build the WebApplication

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) // If running in development environment
            {
                app.UseSwagger(); // Enable Swagger middleware
                app.UseSwaggerUI(); // Enable Swagger UI
            }

            app.UseCors("AllowAll"); // Enable CORS using the "AllowAll" policy

            app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS

            app.UseAuthorization(); // Enable authorization middleware

            app.MapControllers(); // Map controller routes

            app.Run(); // Run the application
        }
    }
}
