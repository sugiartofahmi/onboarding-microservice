using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetService.Domain.Order.Requests
{
    public class OrderCreateRequest
    {
        public Guid UserId { get; set; }
        public int Quantity { get; set; }

        public Guid ProductId { get; set; }
    }
}
