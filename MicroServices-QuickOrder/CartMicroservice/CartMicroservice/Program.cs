using CartMicroservice.DataAccessLayer.EntityFrameworkCore; // Import EF Core data context
using Microsoft.EntityFrameworkCore; // Import EF Core

namespace CartMicroservice
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
            builder.Services.AddScoped<CartMicroservice.ApplicationLayer.RepositoriesContract.ICartRepository, CartMicroservice.DataAccessLayer.Reposotires.CartRepository>(); // Register CartRepository as ICartRepository (scoped lifetime)
            builder.Services.AddScoped<CartMicroservice.ApplicationLayer.ServiceContracts.ICartService, CartMicroservice.ApplicationLayer.Services.CartService>(); // Register CartService as ICartService (scoped lifetime)
            builder.Services.AddDbContext<DataContext>(opt=>opt.UseSqlServer(builder.Configuration.GetConnectionString("dbcs"))); // Register DataContext with SQL Server connection
            //builder.Services.AddControllers().AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            //});

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles; // Ignore cycles in JSON serialization
                options.JsonSerializerOptions.WriteIndented = true; // Optional: pretty print JSON output
            });
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin() // Allow requests from any origin
                          .AllowAnyHeader() // Allow any header
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
            app.UseCors(); // Enable CORS
            app.UseHttpsRedirection(); // Redirect HTTP to HTTPS

            app.UseAuthorization(); // Enable authorization middleware

            app.MapControllers(); // Map controller routes

            app.Run(); // Run the application
        }
    }
}
