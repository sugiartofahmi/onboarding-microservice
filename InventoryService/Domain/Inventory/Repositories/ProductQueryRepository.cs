using DotNetService.Domain.Inventory.Requests;
using DotNetService.Exceptions;
using DotNetService.Http.API.Version1;
using DotNetService.Infrastructure.Databases;

namespace DotNetService.Domain.Inventory.Repositories
{
    public class ProductQueryRepository
    {
        private readonly ProductDBContext _context;

        public ProductQueryRepository(ProductDBContext context)
        {
            _context = context;
        }

        public List<Models.Product> Pagination(ProductQueryRequest queryParams)
        {
            int skip = (queryParams.Page - 1) * queryParams.PerPage;
            var query = _context.Products.AsQueryable();

            query = this.QuerySearch(query, queryParams);
            query = this.QueryFilter(query, queryParams);
            query = this.QuerySort(query, queryParams);

            var data = query.Skip(skip).Take(queryParams.PerPage).ToList();

            return data;
        }

        private IQueryable<Models.Product> QuerySearch(
            IQueryable<Models.Product> query,
            ProductQueryRequest queryParams
        )
        {
            if (queryParams.Search != null)
            {
                query = query.Where(data => data.Name.Contains(queryParams.Search));
            }

            return query;
        }

        private IQueryable<Models.Product> QueryFilter(
            IQueryable<Models.Product> query,
            ProductQueryRequest queryParams
        )
        {
            if (queryParams.Name != null)
            {
                query = query.Where(data => data.Name.Equals(queryParams.Name));
            }

            return query;
        }

        private IQueryable<Models.Product> QuerySort(
            IQueryable<Models.Product> query,
            ProductQueryRequest queryParams
        )
        {
            queryParams.SortBy ??= "updated_at";

            Dictionary<string, Func<Models.Product, object>> sortFunctions =
                new()
                {
                    { "name", data => data.Name },
                    { "updated_at", data => data.UpdatedAt },
                    { "created_at", data => data.CreatedAt },
                };

            if (
                !sortFunctions.TryGetValue(
                    queryParams.SortBy,
                    out Func<Models.Product, object> value
                )
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

        public int Count(ProductQueryRequest queryParams)
        {
            IQueryable<Models.Product> query = _context.Products;

            query = this.QuerySearch(query, queryParams);
            query = this.QueryFilter(query, queryParams);

            return query.Count();
        }

        internal Models.Product Find(Guid id = default)
        {
            return _context.Products.Where(role => role.Id == id).FirstOrDefault();
        }

        public Models.Product FindOneById(Guid id = default, bool isThrowException = false)
        {
            var data = _context.Products.Where(data => data.Id == id).FirstOrDefault();

            if (data == null && isThrowException)
            {
                throw new DataNotFoundException("Product with id " + id + " not found.");
            }
            ;

            return data;
        }

        public Models.Product FindByName(string name)
        {
            Models.Product role = _context
                .Products.Where(role => role.Name == name)
                .FirstOrDefault();
            if (role == null)
            {
                return (new Models.Product());
            }

            return role;
        }

        public bool IsExistsByNameAndIds(string nameRole, Guid[] roleIds)
        {
            return _context
                    .Products.Where(role => role.Name == nameRole)
                    .Where(role => roleIds.Contains(role.Id))
                    .Count() > 0;
        }

        public List<Models.Product> Get(string search, int page, int perPage)
        {
            int skip = (1 - page) * perPage;
            List<Models.Product> roles;
            IQueryable<Models.Product> roleQuery = _context.Products;
            if (search != null)
            {
                roleQuery = roleQuery.Where(role => role.Name.Contains(search));
            }
            roles = roleQuery.Skip(skip).Take(perPage).ToList();
            return roles;
        }

        public int CountAll(string search)
        {
            IQueryable<Models.Product> roleQuery = _context.Products;
            if (search != null)
            {
                roleQuery = roleQuery.Where(role => role.Name.Contains(search));
            }
            return roleQuery.Count();
        }
    }
}
