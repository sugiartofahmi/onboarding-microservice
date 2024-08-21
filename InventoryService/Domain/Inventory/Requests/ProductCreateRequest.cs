using System.ComponentModel.DataAnnotations;

namespace DotNetService.Domain.Inventory.Requests
{
    public class ProductCreateRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Price { get; set; }
    }
}
