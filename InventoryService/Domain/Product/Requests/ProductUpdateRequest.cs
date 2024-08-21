namespace DotNetService.Domain.Product.Requests
{
    public class ProductUpdateRequest : ProductCreateRequest
    {
        public Guid Id { get; set; }
        public int Stock { get; set; }
    }
}
