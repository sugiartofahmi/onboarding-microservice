using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetService.Models
{
    public class Product : Base
    {
        [Column(name: "name")]
        [Required]
        public string Name { get; set; }

        [Column(name: "description")]
        [Required]
        public string Description { get; set; }

        [Column(name: "price")]
        [Required]
        public int Price { get; set; }

        [Column(name: "stock")]
        [Required]
        public int Stock { get; set; }
    }
}
