using Microsoft.AspNetCore.Authentication.JwtBearer; // Import for JWT authentication
using Microsoft.EntityFrameworkCore; // Import for Entity Framework Core
using Microsoft.IdentityModel.Tokens; // Import for token validation
using System.Text; // Import for encoding
using UsersMicroservice.ApplicationLayer.RepositoryContract; // Import repository contract
using UsersMicroservice.ApplicationLayer.ServiceContract; // Import service contract
using UsersMicroservice.ApplicationLayer.Services; // Import services
using UsersMicroservice.DataAccessLayer.EntityFrameworkCore; // Import EF Core data context
using UsersMicroservice.DataAccessLayer.Repositories; // Import repositories

namespace UsersMicroservice // Define the main namespace
{
    public class Program // Main Program class
    {
        public static void Main(string[] args) // Main entry point
        {
            var builder = WebApplication.CreateBuilder(args); // Create a new WebApplication builder with command-line args

            // Add services to the container.

            builder.Services.AddControllers(); // Register controllers for MVC
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer(); // Register endpoint API explorer for Swagger
            builder.Services.AddSwaggerGen(); // Register Swagger generator

            builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("dbcs"))); // Register DataContext with SQL Server connection string

            // Register application services
            builder.Services.AddScoped<IUsersRepo, UsersRepo>(); // Register UsersRepo for IUsersRepo
            builder.Services.AddScoped<ITokenService, TokenService>(); // Register TokenService for ITokenService
            builder.Services.AddScoped<IUsersService, UsersService>(); // Register UsersService for IUsersService

            // JWT Configuration
            var jwtKey = builder.Configuration["Jwt:Key"]; // Get JWT key from configuration
            var jwtIssuer = builder.Configuration["Jwt:Issuer"]; // Get JWT issuer from configuration
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // Add authentication with JWT bearer scheme
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => // Configure JWT bearer options
                {
                    options.TokenValidationParameters = new TokenValidationParameters // Set token validation parameters
                    {
                        ValidateIssuer = true, // Validate the issuer
                        ValidateAudience = true, // Validate the audience
                        ValidateLifetime = true, // Validate token lifetime
                        ValidateIssuerSigningKey = true, // Validate the signing key
                        ValidIssuer = jwtIssuer, // Set valid issuer
                        ValidAudience = jwtIssuer, // Set valid audience
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!)) // Set the signing key
                    };
                });

            var app = builder.Build(); // Build the WebApplication

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) // If in development environment
            {
                app.UseSwagger(); // Enable Swagger middleware
                app.UseSwaggerUI(); // Enable Swagger UI
            }

            app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS

            app.UseAuthentication(); // Ensure authentication middleware is added
            app.UseAuthorization(); // Enable authorization middleware

            app.MapControllers(); // Map controller endpoints

            app.Run(); // Run the application
        }
    }
}
