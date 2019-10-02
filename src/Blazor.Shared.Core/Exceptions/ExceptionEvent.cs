using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.Shared.Core.Exceptions
{
    public enum ExceptionEvent
    {
        NotFound,
        InvalidParameters,
        LoginFailed,
        RegistrationFailed,
        UploadFailed,
        AccessDenied
    }
}
