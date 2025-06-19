namespace OrdersMicroservice.DomainLayer.Models
{
    public class Response<T>
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public T? Data { get;set; }

    }
}
