using DotNetService.Models;

namespace DotNetService.Domain.Order.Requests
{
    public class OrderUpdateRequest : OrderCreateRequest
    {
        public OrderStatusEnum Status { get; set; }

        public Guid Id { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
