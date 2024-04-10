namespace FeedbackService.Domain.Shared
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; }
    }
}