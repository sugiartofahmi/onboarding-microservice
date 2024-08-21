using DotNetService.Models;

namespace DotNetService.Domain.Order.Requests
{
    public class OrderCreateRequest
    {
        public Guid UserId { get; set; }
        public int Quantity { get; set; }

        public Guid ProductId { get; set; }
    }
}
