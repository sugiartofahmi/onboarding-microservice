using System.ComponentModel.DataAnnotations;

namespace DotNetService.Http.API.Version1.Order.Requests
{
    public class OrderCreateRequest
    {
        public Guid? UserId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public Guid ProductId { get; set; }
    }

    public enum OrderStatusEnum
    {
        Pending,
        Accepted,
        Rejected
    }
}
