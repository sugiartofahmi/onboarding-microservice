using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using DotNetService.Exceptions;
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
    }
}
