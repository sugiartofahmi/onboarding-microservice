using Microsoft.AspNetCore.Mvc;

namespace DotNetService.Http.API.Version1.Inventory.Requests
{
    public class ProductQueryRequest : Query
    {
        [FromQuery(Name = "name")]
        public string Name { get; set; }
    }
}
