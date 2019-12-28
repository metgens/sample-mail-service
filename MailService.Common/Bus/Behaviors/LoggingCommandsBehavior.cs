using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MailService.Common.Bus.Command;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MailService.Common.Bus.Behaviors
{
    public class LoggingCommandsBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand
    {
        private readonly ILogger<LoggingCommandsBehavior<TRequest, TResponse>> _logger;

        public LoggingCommandsBehavior(ILogger<LoggingCommandsBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestName = typeof(TRequest).Name;

            var sw = new Stopwatch();
            sw.Start();

            _logger.LogTrace($"Command handling {requestName}: {JsonConvert.SerializeObject(request)}");
            var result = await next();
            _logger.LogTrace($"Command handled {requestName} in {sw.Elapsed}: {JsonConvert.SerializeObject(result)}");

            sw.Stop();

            return result;
        }
    }

}
