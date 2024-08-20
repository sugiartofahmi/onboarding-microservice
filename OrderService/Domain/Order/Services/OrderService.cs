using DotNetService.Domain.Order.Repositories;
using DotNetService.Domain.Order.Requests;
using DotNetService.Http.API.Version1;

namespace DotNetService.Domain.Order.Services
{
    public class OrderService
    {
        private readonly OrderQueryRepository _orderQueryRepository;

        private readonly OrderStoreRepository _orderStoreRepository;

        public OrderService(
            OrderQueryRepository orderQueryRepository,
            OrderStoreRepository orderStoreRepository
        )
        {
            _orderQueryRepository = orderQueryRepository;

            _orderStoreRepository = orderStoreRepository;
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

        public void Create(OrderCreateRequest request)
        {
            Models.Order data = new Models.Order
            {
                UserId = request.UserId,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                Status = Models.OrderStatusEnum.Pending,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            _orderStoreRepository.Create(data);
        }

        public List<Models.Order> GetAllHasStatusPending()
        {
            return _orderQueryRepository.GetAllHasStatusPending();
        }

        public void Update(OrderCreateRequest request)
        {
            Models.Order data = new Models.Order
            {
                UserId = request.UserId,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                Status = Models.OrderStatusEnum.Pending,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            _orderStoreRepository.Update(data);
        }
    }
}
