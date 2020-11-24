using IdentityAuthencation.Logger;
using System;
using System.Globalization;

namespace IdentityAuthencation.Helpers
{
    public class AppException : Exception
    {
        public AppException(ILoggerManager logger) : base()
        {
        }

        public AppException(ILoggerManager logger, string message) : base(message)
        {
            logger.LogError(message);
        }

        public AppException(ILoggerManager logger, string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
            logger.LogError(String.Format(CultureInfo.CurrentCulture, message, args));
        }
    }
}
