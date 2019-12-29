using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MailService.Common.Bus.Command;
using MailService.Common.Bus.Query;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MailService.Api.Logging
{
    public class LoggingQueriesBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IQuery<TResponse>
    {
        private readonly ILogger<LoggingQueriesBehavior<TRequest, TResponse>> _logger;

        public LoggingQueriesBehavior(ILogger<LoggingQueriesBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName = typeof(TRequest).Name;

            var sw = new Stopwatch();
            sw.Start();

            _logger.LogTrace($"Query handling {requestName}: {JsonConvert.SerializeObject(request)}");
            var result = await next();
            _logger.LogTrace($"Query handled {requestName} in {sw.Elapsed}");

            sw.Stop();

            return result;
        }
    }


}
