using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetService.Domain.Product.Requests
{
    public class ProductUpdateRequest : ProductCreateRequest
    {
        public Guid Id { get; set; }
        public int Stock { get; set; }
    }
}
