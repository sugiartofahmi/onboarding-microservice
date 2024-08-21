using DotNetService.Models;

namespace DotNetService.Domain.Order.Requests
{
    public class OrderUpdateRequest : OrderCreateRequest
    {
        public OrderStatusEnum Status { get; set; }
    }
}
