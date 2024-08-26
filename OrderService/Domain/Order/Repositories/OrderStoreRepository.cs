using DotNetService.Infrastructure.Databases;

namespace DotNetService.Domain.Order.Repositories
{
    public class OrderStoreRepository
    {
        private readonly OrderQueryRepository _orderQueryRepository;
        private readonly OrderDBContext _context;

        public OrderStoreRepository(
            OrderQueryRepository orderQueryRepository,
            OrderDBContext context
        )
        {
            _orderQueryRepository = orderQueryRepository;
            _context = context;
        }

        public async Task Create(Models.Order data)
        {
            _context.Orders.Add(data);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            Models.Order data = await _orderQueryRepository.FindOneById(id, true);

            _context.Orders.Remove(data);
            int affectedRows = await _context.SaveChangesAsync();

            if (affectedRows == 0)
            {
                throw new UnprocessableEntityException("No data was deleted.");
            }
        }

        public async Task Update(Models.Order data)
        {
            var existingOrder = await _orderQueryRepository.FindOneById(data.Id, true);

            if (existingOrder != null)
            {
                _context.Entry(existingOrder).CurrentValues.SetValues(data);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new UnprocessableEntityException("No data was updated.");
            }
        }
    }
}
