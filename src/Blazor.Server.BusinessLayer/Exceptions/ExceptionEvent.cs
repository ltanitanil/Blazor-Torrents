using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.Server.BusinessLayer.Exceptions
{
    public enum ExceptionEvent
    {
        NotFound,
        InvalidParameters,
        LoginFailed,
        RegistrationFailed
    }
}
