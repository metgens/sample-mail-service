using MailService.Api.Exceptions;
using MailService.Common.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailService.Api.Exceptions
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IHostingEnvironment _env;
        private ILogger _logger;

        public GlobalExceptionFilter(ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            _logger = _loggerFactory.CreateLogger("GlobalExceptionFilter");
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            if (exception is AppException)
            {
                var appException = exception as AppException;
                var result = new ObjectResult(appException.ToErrorResponse(_env.IsDevelopment()))
                {
                    StatusCode = (int)appException.ErrorCode
                };
                context.Result = result;
            }
            else
            {
                var result = new ObjectResult(exception.ToErrorResponse(ErrorCode.Unexpected, _env.IsDevelopment()))
                {
                    StatusCode = (int)ErrorCode.Unexpected
                };
                context.Result = result;
            }

            _logger.LogError(exception, exception.GetExceptionMessages());
        }
    }

}
