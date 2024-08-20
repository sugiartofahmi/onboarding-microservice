using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetService.Models;

namespace DotNetService.Domain.Order.Requests
{
    public class OrderUpdateRequest : OrderCreateRequest
    {
        public OrderStatusEnum Status { get; set; }
    }
}
