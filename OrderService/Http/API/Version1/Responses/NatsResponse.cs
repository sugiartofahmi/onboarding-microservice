namespace DotNetService.Http.API.Version1.Responses
{
    public class NatsResponse<T>
    {
        public T? result { get; set; }
    }

    public class NatsResponseData
    {
        public string? result { get; set; }
    }
}
