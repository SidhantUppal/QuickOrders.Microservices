using Ocelot.DependencyInjection; // Import Ocelot for dependency injection
using Ocelot.Middleware; // Import Ocelot middleware

namespace ApiGateWayMicroservice
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Create a new WebApplication builder with command-line arguments
            var builder = WebApplication.CreateBuilder(args);

            // Add Ocelot configuration file (ocelot.json) to the configuration system
            builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

            // Register Ocelot services for API Gateway functionality
            builder.Services.AddOcelot();

            // Register Swagger/OpenAPI services for API documentation (for debugging, not for gateway routing)
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register CORS policy to allow any origin, header, and method
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // Build the WebApplication instance
            var app = builder.Build();

            // Enable CORS middleware
            app.UseCors();

            // Enable Swagger UI and generator in development environment only
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Enable HTTPS redirection (optional for gateway, but recommended if using HTTPS)
            app.UseHttpsRedirection();

            // No controllers or authorization middleware needed unless you add custom endpoints
            // app.UseAuthorization();
            // app.MapControllers();

            // Start Ocelot middleware to handle API Gateway routing
            await app.UseOcelot();

            // Run the application
            app.Run();
        }
    }
}

// The following is a commented-out alternative Program.cs template for reference
//using Ocelot.DependencyInjection;
//using Ocelot.Middleware;

//namespace ApiGateWayMicroservice
//{
//    public class Program
//    {
//        public static async Task Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            // Add services to the container.

//            builder.Services.AddControllers();
//            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//            builder.Services.AddEndpointsApiExplorer();
//            builder.Services.AddSwaggerGen();
//            // Read ocelot.json
//            builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
//            builder.Services.AddOcelot();
//            var app = builder.Build();

//            // Configure the HTTP request pipeline.
//            if (app.Environment.IsDevelopment())
//            {
//                app.UseSwagger();
//                app.UseSwaggerUI();
//            }

//            app.UseHttpsRedirection();

//            app.UseAuthorization();


//            app.MapControllers();
//            await app.UseOcelot();
//            app.Run();
//        }
//    }
//}
