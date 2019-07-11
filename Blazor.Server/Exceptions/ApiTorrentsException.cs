using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.Server.Exceptions
{
    public class ApiTorrentsException: Exception
    {
        public ExceptionEvent ExceptionEvent { get; private set; }

        public ApiTorrentsException(ExceptionEvent exception) : base()
        {
            ExceptionEvent = exception;
        }

        public ApiTorrentsException(ExceptionEvent exception, string message):base(message)
        {
            ExceptionEvent = exception;
        }

    }
}
