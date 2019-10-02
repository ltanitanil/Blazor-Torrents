using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Blazor.Shared.Core.Exceptions;

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
            {
                ExceptionEvent.RegistrationFailed,
                StatusCodes.Status400BadRequest
            },
            {
                ExceptionEvent.LoginFailed,
                StatusCodes.Status400BadRequest
            },
            {
                ExceptionEvent.AccessDenied,
                StatusCodes.Status403Forbidden
            }
        };

        private readonly ILogger<ApiExceptionFilterAttribute> _logger;

        public ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }

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

        private void HandleException(ExceptionContext context)
        {
            context.Result = new ObjectResult(context.Exception.Message)
            {
                StatusCode = (context.Exception is AppException exception)
                                ? exceptionFilter[exception.ExceptionEvent]
                                : StatusCodes.Status500InternalServerError
            };

            _logger.LogError(context.Exception, context.Exception.Message);
        }
    }
}
