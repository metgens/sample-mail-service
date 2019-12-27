using System;
using System.Collections.Generic;
using System.Text;

namespace MailService.Common.Dates
{
    public static class SystemTimeOffset
    {
        private static DateTimeOffset? _utcNow;

        public static DateTimeOffset UtcNow
        {
            get
            {
                if (_utcNow != null)
                    return _utcNow.Value;
                else
                    return DateTimeOffset.UtcNow;
            }
        }

        public static void SetDateTimeOffset(DateTimeOffset DateTimeOffsetNow)
        {
            _utcNow = DateTimeOffsetNow;
        }

        public static void ResetDateTimeOffset()
        {
            _utcNow = null;
        }
    }

}
