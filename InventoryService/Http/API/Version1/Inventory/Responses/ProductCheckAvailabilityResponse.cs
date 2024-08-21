namespace DotNetService.Http.API.Version1.Inventory.Requests
{
    public class ProductCheckAvailabilityResponse
    {
        public string OrderId { get; set; }
        public bool IsProductAvailable { get; set; }
    }
}
