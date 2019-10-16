using System;

namespace Blazor.Shared.Core.Exceptions
{
    public class AppException:Exception
    {
        public ExceptionEvent ExceptionEvent { get; private set; }

        public AppException(ExceptionEvent exception) : this(exception, null)
        {
        }

        public AppException(ExceptionEvent exception, string message) : base(message)
        {
            ExceptionEvent = exception;
        }
    }
}
