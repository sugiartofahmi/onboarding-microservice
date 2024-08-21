using System.Net;
using DotNetService.Domain.Product.Services;
using DotNetService.Http.API.Version1.Product.Requests;
using DotNetService.Infrastructure.Shareds;
using Microsoft.AspNetCore.Mvc;

namespace DotNetService.Http.API.Version1.Product.Controllers
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductController(ProductService productService) : ControllerBase
    {
        private readonly ProductService _productService = productService;

        [HttpGet]
        public async Task<ApiResponsePagination> Index(ProductQueryRequest request)
        {
            PaginationModel result = await _productService.Index(request);

            return new ApiResponsePagination(HttpStatusCode.OK, result);
        }
    }
}
