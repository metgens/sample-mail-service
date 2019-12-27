using MailService.Common.Dates;
using MailService.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailService.Domain.Base
{
    public class BaseEntity
    {
        public DateTimeOffset CreatedDate { get; protected set; }
        public DateTimeOffset? UpdatedDate { get; protected set; }

        public BaseEntity()
        {
            CreatedDate = SystemTimeOffset.UtcNow;
            SetUpdatedDate();
        }

        protected virtual void SetUpdatedDate(DateTimeOffset? date = null)
            => UpdatedDate = date ?? SystemTimeOffset.UtcNow;

        protected void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw AppException.BusinessRuleBroken(rule);
            }
        }
    }
}
