using DotNetService.Http.API.Version1;
using Microsoft.AspNetCore.Mvc;

namespace DotNetService.Domain.Product.Requests
{
    public class ProductQueryRequest : Query
    {
        [FromQuery(Name = "name")]
        public string Name { get; set; }
    }
}
