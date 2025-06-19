using Microsoft.IdentityModel.Tokens; // Import for security key and signing credentials
using System.IdentityModel.Tokens.Jwt; // Import for JWT token handling
using System.Security.Claims; // Import for claims in JWT
using System.Text; // Import for encoding
using UsersMicroservice.ApplicationLayer.ServiceContract; // Import token service contract interface
using UsersMicroservice.DomainLayer.Models; // Import User domain model

namespace UsersMicroservice.ApplicationLayer.Services // Define the namespace for services
{
    public class TokenService: ITokenService // TokenService implements ITokenService interface
    {
        private readonly IConfiguration _config; // Private field for configuration dependency

        public TokenService(IConfiguration config) // Constructor with dependency injection for configuration
        {
            _config = config; // Assign injected configuration to private field
        }

        public string GenerateToken(User user) // Method to generate JWT token for a user
        {
            var claims = new[] // Define claims for the token
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email), // Subject claim with user's email
                new Claim("username", user.Username), // Custom claim for username
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Unique identifier for the token
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)); // Create symmetric security key from config
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // Create signing credentials with HMAC SHA256

            var token = new JwtSecurityToken( // Create JWT token
                issuer: _config["Jwt:Issuer"], // Set issuer from config
                audience: _config["Jwt:Issuer"], // Set audience from config
                claims: claims, // Set claims
                expires: DateTime.UtcNow.AddHours(1), // Set expiration time to 1 hour from now
                signingCredentials: creds // Set signing credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token); // Write and return the serialized JWT token
        }

    }
}
