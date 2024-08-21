using Microsoft.AspNetCore.Mvc;

namespace DotNetService.Http.API.Version1.Product.Requests
{
    public class ProductQueryRequest : Query
    {
        [FromQuery(Name = "name")]
        public string Name { get; set; }
    }
}
