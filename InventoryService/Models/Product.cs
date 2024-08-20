using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetService.Models
{
    public class Product : Base
    {
        [Column(name: "name")]
        [Required]
        public string name { get; set; }

        [Column(name: "description")]
        [Required]
        public string description { get; set; }

        [Column(name: "price")]
        [Required]
        public int price { get; set; }

        [Column(name: "stock")]
        [Required]
        public int stock { get; set; }
    }
}
