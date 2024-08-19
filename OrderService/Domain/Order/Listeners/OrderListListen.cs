using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetService.Constants.Logger;
using DotNetService.Domain.Logging.Services;
using DotNetService.Infrastructure.Subscriptions;

namespace DotNetService.Domain.Order.Listeners
{
    public class OrderListListen(ILoggerFactory loggerFactory, LoggingService loggingService)
        : IReplyAction<IDictionary<string, object>, IDictionary<string, object>>
    {
        public readonly ILogger _logger = loggerFactory.CreateLogger(LoggerConstant.NATS);
        public readonly LoggingService _loggingService = loggingService;

        public IDictionary<string, object> Reply(IDictionary<string, object> data)
        {
            // EXAMPLE: Do operation for reply event
            Console.WriteLine("Data List:");
            Console.WriteLine(string.Join(", ", data.Select(kvp => $"{kvp.Key}: {kvp.Value}")));
            var reply = new Dictionary<string, object> { { "status", "OK Bro" }, };

            return reply;
        }
    }
}
