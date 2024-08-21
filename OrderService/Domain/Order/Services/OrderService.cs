using DotNetService.Constants.Event;
using DotNetService.Domain.Order.Repositories;
using DotNetService.Domain.Order.Requests;
using DotNetService.Http.API.Version1;
using DotNetService.Http.API.Version1.Order.Requests;
using DotNetService.Infrastructure.Integrations.NATs;
using DotNetService.Infrastructure.Shareds;

namespace DotNetService.Domain.Order.Services
{
    public class OrderService
    {
        private readonly OrderQueryRepository _orderQueryRepository;

        private readonly OrderStoreRepository _orderStoreRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly NATsIntegration _natsIntegration;

        public OrderService(
            OrderQueryRepository orderQueryRepository,
            OrderStoreRepository orderStoreRepository,
            IHttpContextAccessor httpContextAccessor,
            NATsIntegration natsIntegration
        )
        {
            _orderQueryRepository = orderQueryRepository;

            _orderStoreRepository = orderStoreRepository;

            _httpContextAccessor = httpContextAccessor;

            _natsIntegration = natsIntegration;
        }

        public PaginationModel Index(Query query = null)
        {
            var data = this.Pagination(query);
            int count = _orderQueryRepository.Count(query);
            decimal pageInCount = ((decimal)count) / query.PerPage;
            PaginationModel paginate =
                new()
                {
                    TotalPage = (int)Math.Ceiling(pageInCount),
                    Page = query.Page,
                    PerPage = query.PerPage,
                    Data = data,
                    Total = count
                };
            return paginate;
        }

        public List<Models.Order> Pagination(Query query = null)
        {
            return _orderQueryRepository.Pagination(query);
        }

        public Models.Order DetailById(Guid id)
        {
            return _orderQueryRepository.FindOneById(id);
        }

        public void Delete(Guid id)
        {
            _orderStoreRepository.Delete(id);
        }

        public async Task Create(OrderCreateRequest request)
        {
            //Create order
            Models.Order data = new Models.Order
            {
                Id = Guid.NewGuid(),
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                Status = Models.OrderStatusEnum.Pending,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                // UserId = new Guid(_httpContextAccessor?.HttpContext?.User.FindFirst("id")?.Value)
                UserId = Guid.NewGuid()
            };

            await _orderStoreRepository.Create(data);

            // Pulblish event to get product availability
            string subject = _natsIntegration.Subject(
                NATsEventModuleEnum.INVENTORY_PRODUCT,
                NATsEventActionEnum.GET,
                NATsEventStatusEnum.INFO
            );

            var dataRequest = new OrderCheckProductRequest
            {
                OrderId = data.Id,
                ProductId = request.ProductId,
                Quantity = request.Quantity
            };

            await _natsIntegration.Publish<string>(subject, Utils.JsonSerialize(dataRequest));
        }

        public List<Models.Order> GetAllHasStatusPending()
        {
            return _orderQueryRepository.GetAllHasStatusPending();
        }

        public void Update(OrderUpdateRequest request)
        {
            var existingOrder = _orderQueryRepository.FindOneById(request.Id);
            if (existingOrder == null)
            {
                throw new UnprocessableEntityException("Order not found.");
            }

            existingOrder.UserId = request.UserId;
            existingOrder.ProductId = request.ProductId;
            existingOrder.Quantity = request.Quantity;
            existingOrder.Status = request.Status;
            existingOrder.UpdatedAt = DateTime.Now;

            _orderStoreRepository.Update(existingOrder);
        }
    }
}
