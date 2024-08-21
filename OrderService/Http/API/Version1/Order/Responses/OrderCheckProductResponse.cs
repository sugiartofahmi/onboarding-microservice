using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetService.Http.API.Version1.Order.Responses
{
    public class OrderCheckProductResponse
    {
        public string OrderId { get; set; }
        public bool IsProductAvailable { get; set; }
    }
}
