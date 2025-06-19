using Microsoft.Identity.Client;
using UsersMicroservice.DomainLayer.Models;

namespace UsersMicroservice.ApplicationLayer.Dto
{
    public class Response
    {
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public User? User { get; set; } = null;

    }

    public class Response<T>
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public T? Data { get; set; }
    }

}
