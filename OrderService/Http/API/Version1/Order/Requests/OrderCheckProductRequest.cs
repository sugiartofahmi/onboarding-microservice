namespace DotNetService.Http.API.Version1.Order.Requests
{
    public class OrderCheckProductRequest
    {
        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
