using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetService.Http.API.Version1.Responses
{
    public class NatsResponse<T>
    {
        public T result { get; set; }
    }
}
