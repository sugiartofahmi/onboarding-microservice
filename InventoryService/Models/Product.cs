using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetService.Models
{
    public class Product : Base
    {
        [Column(name: "name")]
        public string Name { get; set; }

        [Column(name: "description")]
        public string Description { get; set; }

        [Column(name: "price")]
        public int? Price { get; set; }

        [Column(name: "stock")]
        public int? Stock { get; set; }
    }
}
