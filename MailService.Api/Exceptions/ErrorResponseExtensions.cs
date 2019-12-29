using MailService.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Api.Exceptions
{
    public static class ErrorResponseExtensions
    {

        /// <summary>
        /// Convert exception into error response structure
        /// </summary>
        /// <param name="exception">Source exception</param>
        /// <param name="withDebugInfo">If true - stack trace is filled in</param>
        /// <returns></returns>
        public static ErrorResponse ToErrorResponse(this AppException exception, bool withDebugInfo)
        {
            var response = new ErrorResponse
            {
                UserMessage = exception.UserMessage,
                Message = withDebugInfo ? GetExceptionMessages(exception) : null,
                Code = exception.ErrorCode.ToString()
            };

            if (withDebugInfo)
                response.StackTrace = exception.StackTrace;

            return response;
        }

        public static ErrorResponse ToErrorResponse(this Exception exception, ErrorCode errorCode, bool withDebugInfo, string userMessage = null, Guid? guid = null)
        {
            var response = new ErrorResponse
            {
                UserMessage = userMessage ?? "Strange. An Error has occured. If this will happen again please contact our help desk.",
                Message = withDebugInfo ? GetExceptionMessages(exception) : null,
                Code = errorCode.ToString()
            };

            if (withDebugInfo)
                response.StackTrace = exception.StackTrace;

            return response;
        }

        public static string GetExceptionMessages(this Exception e, string msgs = "")
        {
            var ae = e as AppException;

            if (e == null) return string.Empty;
            if (msgs == "")
            {
                var sb = new StringBuilder();

                if (ae?.ErrorCode != null)
                    sb.Append($"[{ae.ErrorCode}] ");

                sb.Append(e.Message);

                if (ae?.UserMessage != null)
                    sb.Append($"; {ae.UserMessage}");

                sb.Append(";");

                msgs = sb.ToString();
            }
            if (e.InnerException != null)
                msgs += " InnerException: " + GetExceptionMessages(e.InnerException);
            return msgs;
        }
    }
    /// <summary>
    /// Generic error response
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Message to user, can be displayed as for example toast text
        /// </summary>
        public string UserMessage { get; set; }
        /// <summary>
        /// Error message for developer
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Error code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Stack trace, filled in only in debug environment
        /// </summary>
        public string StackTrace { get; set; }
    }

}
