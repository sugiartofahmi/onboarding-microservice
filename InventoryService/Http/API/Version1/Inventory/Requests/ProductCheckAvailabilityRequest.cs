namespace DotNetService.Http.API.Version1.Inventory.Requests
{
    public class ProductCheckAvailabilityRequest
    {
        public string OrderId { get; set; }

        public string ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
