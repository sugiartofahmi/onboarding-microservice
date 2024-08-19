using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetService.Http.API.Version1;

namespace DotNetService.Domain.Order.Repositories
{
    public class OrderQueryRepository
    {
        public async Task<List<Models.Order>> Pagination(Query query)
        {
            return new List<Models.Order>();
        }
    }
}
