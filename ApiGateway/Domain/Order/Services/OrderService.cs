using System.Net;
using DotNetService.Constants.Event;
using DotNetService.Http.API.Version1;
using DotNetService.Http.API.Version1.Order.Requests;
using DotNetService.Http.API.Version1.Responses;
using DotNetService.Infrastructure.Integrations.NATs;
using DotNetService.Infrastructure.Shareds;
using Microsoft.AspNetCore.Mvc;

namespace DotNetService.Domain.Order.Services
{
    public class OrderService(NATsIntegration natsIntegration)
    {
        private readonly NATsIntegration _natsIntegration = natsIntegration;

        public async Task<ApiResponsePagination> Index(Query request)
        {
            string subject = _natsIntegration.Subject(
                NATsEventModuleEnum.ORDER,
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

        public async Task<object> Detail(Guid id)
        {
            string subject = _natsIntegration.Subject(
                NATsEventModuleEnum.ORDER,
                NATsEventActionEnum.GET_BY_ID,
                NATsEventStatusEnum.REQUEST
            );
            var result = await _natsIntegration.PublishAndGetReply<string, NatsResponse<object>>(
                subject,
                Utils.JsonSerialize(new { id })
            );

            return result?.result;
        }

        public async Task Create(OrderCreateRequest request)
        {
            string subject = _natsIntegration.Subject(
                NATsEventModuleEnum.ORDER,
                NATsEventActionEnum.CREATE,
                NATsEventStatusEnum.SUCCESS
            );

            await _natsIntegration.Publish<string>(subject, Utils.JsonSerialize(request));
        }
    }
}
