using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetService.Models
{
    [Table("orders")]
    public class Order : Base
    {
        [Column(name: "user_id")]
        [Required]
        public Guid UserId { get; set; }

        [Column(name: "quantity")]
        [Required]
        public int Quantity { get; set; }

        [Column(name: "product_id")]
        [Required]
        public Guid ProductId { get; set; }

        [Column(name: "status")]
        public OrderStatusEnum? Status { get; set; }
    }

    public enum OrderStatusEnum
    {
        Pending,
        Accepted,
        Rejected
    }
}
