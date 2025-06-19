using System.ComponentModel.DataAnnotations.Schema;

namespace UsersMicroservice.DomainLayer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        [NotMapped]
        public string Token { get; set; } = string.Empty;
    }
}
