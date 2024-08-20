using DotNetService.Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

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

        public void Create(Models.Order data)
        {
            _context.Orders.Add(data);
            _context.SaveChangesAsync();
        }

        public void Delete(Guid id)
        {
            Models.Order data = _orderQueryRepository.FindOneById(id, true);

            _context.Orders.Remove(data);
            int affectedRows = _context.SaveChanges();

            if (affectedRows == 0)
            {
                throw new UnprocessableEntityException("No data was deleted.");
            }
        }

        public void Update(Models.Order data)
        {
            _context.Entry(data).State = EntityState.Modified;
            _context.SaveChangesAsync();
        }
    }
}
