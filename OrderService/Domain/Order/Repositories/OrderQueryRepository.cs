using DotNetService.Exceptions;
using DotNetService.Http.API.Version1;
using DotNetService.Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

namespace DotNetService.Domain.Order.Repositories
{
    public class OrderQueryRepository
    {
        private readonly OrderDBContext _context;

        public OrderQueryRepository(OrderDBContext context)
        {
            _context = context;
        }

        public List<Models.Order> Pagination(Query queryParams)
        {
            int skip = (queryParams.Page - 1) * queryParams.PerPage;
            var query = _context.Orders.AsQueryable();

            query = this.QuerySort(query, queryParams);

            var data = query.Skip(skip).Take(queryParams.PerPage).ToList();

            return data;
        }

        private IQueryable<Models.Order> QuerySort(
            IQueryable<Models.Order> query,
            Query queryParams
        )
        {
            queryParams.SortBy ??= "updated_at";

            Dictionary<string, Func<Models.Order, object>> sortFunctions =
                new()
                {
                    { "updated_at", data => data.UpdatedAt },
                    { "created_at", data => data.CreatedAt },
                };

            if (
                !sortFunctions.TryGetValue(queryParams.SortBy, out Func<Models.Order, object> value)
            )
            {
                throw new BadHttpRequestException(
                    $"Invalid sort column: {queryParams.SortBy}, available sort columns: "
                        + string.Join(", ", sortFunctions.Keys)
                );
            }

            query =
                queryParams.Order == SortOrderEnum.Asc
                    ? query.OrderBy(value).AsQueryable()
                    : query.OrderByDescending(value).AsQueryable();

            return query;
        }

        public int Count(Query queryParams)
        {
            IQueryable<Models.Order> query = _context.Orders;

            return query.Count();
        }

        internal Models.Order Find(Guid id = default)
        {
            return _context.Orders.Where(role => role.Id == id).FirstOrDefault();
        }

        public Models.Order FindOneById(Guid id = default, bool isThrowException = false)
        {
            var data = _context.Orders.Where(data => data.Id == id).FirstOrDefault();

            if (data == null && isThrowException)
            {
                throw new DataNotFoundException("Data with id " + id + " not found.");
            }
            ;

            return data;
        }

        public List<Models.Order> Get(string search, int page, int perPage)
        {
            int skip = (1 - page) * perPage;
            List<Models.Order> roles;
            IQueryable<Models.Order> roleQuery = _context.Orders;

            roles = roleQuery.Skip(skip).Take(perPage).ToList();
            return roles;
        }

        public int CountAll(string search)
        {
            IQueryable<Models.Order> roleQuery = _context.Orders;

            return roleQuery.Count();
        }

        public List<Models.Order> GetAllHasStatusPending()
        {
            return _context
                .Orders.AsNoTracking()
                .Where(o => o.Status == Models.OrderStatusEnum.Pending)
                .ToList();
        }
    }
}
