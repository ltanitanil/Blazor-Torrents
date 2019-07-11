
using Blazor.Server.Exceptions;
using Blazor.Shared.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Blazor.Server.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private static readonly Dictionary<ExceptionEvent, IActionResult> exceptionFilter = new Dictionary<ExceptionEvent, IActionResult>()
        {
            {
                ExceptionEvent.IndexOutOfRange,
                new StatusCodeResult(400)
            },
            {
                ExceptionEvent.NotFound,
                new StatusCodeResult(404)
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

        private static void HandleException(ExceptionContext context) =>
            context.Result = (context.Exception is ApiTorrentsException exception) ? exceptionFilter[exception.ExceptionEvent] : new StatusCodeResult(500);

    }
}
