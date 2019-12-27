using System;

namespace MailService.Common.Exceptions
{
    public class AppException : Exception
    {
        public string UserMessage { get; set; }
        public ErrorCode ErrorCode { get; set; }

        public AppException(string errorMessage, string userMessage, ErrorCode errorCodeEnum, Exception innerException = null) : base(errorMessage, innerException)
        {
            ErrorCode = errorCodeEnum;
            UserMessage = userMessage;
        }

        public AppException(string userMessage, ErrorCode errorCodeEnum, Exception innerException = null) : base(userMessage, innerException)
        {
            ErrorCode = errorCodeEnum;
            UserMessage = userMessage;
        }

        public static AppException NotExisting(string entityName, params object[] ids)
        {
            var key = ids != null ? string.Join(",", ids) : null;
            return new AppException($"There is no entity '{entityName}' with key: '{key}'", "The requested resource does not exist", ErrorCode.NotFound);
        }

        public static AppException Duplicate(string entityName, params object[] ids)
        {
            var key = ids != null ? string.Join(",", ids) : null;
            return new AppException($"Entity '{entityName}' with key: '{key}' already exists", "The requested resource already exists", ErrorCode.Conflict);
        }

        public static AppException BusinessRuleBroken(IBusinessRule rule, params object[] ids)
        {
            var key = ids != null ? string.Join(",", ids) : null;
            return new AppException($"Business rule '{rule.GetType().FullName}' broken for entity with key: '{key}'. {rule.Message}", rule.Message ?? $"Business rule '{rule.GetType().FullName}' broken.", ErrorCode.Conflict);
        }
    }
}
