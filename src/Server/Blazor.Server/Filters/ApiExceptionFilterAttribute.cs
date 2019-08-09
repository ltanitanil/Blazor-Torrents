using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.Server.WebApi.Exceptions;

namespace Blazor.Server.WebApi.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private static readonly Dictionary<ExceptionEvent, int> exceptionFilter = new Dictionary<ExceptionEvent, int>()
        {
            {
                ExceptionEvent.InvalidParameters,
                StatusCodes.Status400BadRequest
            },
            {
                ExceptionEvent.NotFound,
                StatusCodes.Status404NotFound
            },
        };

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            HandleException(context);
            return base.OnExceptionAsync(context);
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);
            base.OnException(context);
        }

        private static void HandleException(ExceptionContext context) => context.Result =
            new ObjectResult(context.Exception.Message) { StatusCode = (context.Exception is ApiTorrentsException exception) ? exceptionFilter[exception.ExceptionEvent] : StatusCodes.Status500InternalServerError };

    }
}
