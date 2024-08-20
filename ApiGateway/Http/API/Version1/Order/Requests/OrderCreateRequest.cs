using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
