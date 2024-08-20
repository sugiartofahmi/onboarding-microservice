using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DotNetService.Constants.Event;
using DotNetService.Http.API.Version1;
using DotNetService.Http.API.Version1.Product.Requests;
using DotNetService.Http.API.Version1.Responses;
using DotNetService.Infrastructure.Integrations.NATs;
using DotNetService.Infrastructure.Shareds;

namespace DotNetService.Domain.Product.Services
{
    public class ProductService(NATsIntegration natsIntegration)
    {
        private readonly NATsIntegration _natsIntegration = natsIntegration;

        public async Task<ApiResponsePagination> Index(ProductQueryRequest request)
        {
            string subject = _natsIntegration.Subject(
                NATsEventModuleEnum.PRODUCT,
                NATsEventActionEnum.GET,
                NATsEventStatusEnum.REQUEST
            );

            var result = await _natsIntegration.PublishAndGetReply<
                string,
                NatsResponse<PaginationModel>
            >(subject, Utils.JsonSerialize(request));

            var repliedData = result.result;

            return new ApiResponsePagination(
                HttpStatusCode.OK,
                new PaginationModel
                {
                    Data = repliedData.Data,
                    Total = repliedData.Total,
                    Page = repliedData.Page,
                    PerPage = repliedData.PerPage,
                    TotalPage = repliedData.TotalPage
                }
            );
        }
    }
}
