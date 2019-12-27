using System;
using System.Collections.Generic;
using System.Text;

namespace MailService.Common.Exceptions
{
    public interface IBusinessRule
    {
        bool IsBroken();
        string Message { get; }
    }
}
