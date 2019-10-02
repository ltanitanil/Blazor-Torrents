using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.Frontend.BusinessLayer.Exceptions
{
    public class ResponseException : Exception
    {
        public ResponseException(string message) : base(message)
        {

        }
    }
}
