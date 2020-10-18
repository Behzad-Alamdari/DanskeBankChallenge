using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DBC.WebApi.ActionFilters
{
    public class LoggerActionFilter : IAsyncActionFilter
    {
        private readonly ILogger<LoggerActionFilter> _logger;

        public LoggerActionFilter(ILogger<LoggerActionFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var httpContext = context.HttpContext;
            var method = httpContext.Request.Method;
            var controller = actionDescriptor?.ControllerName;
            var action = actionDescriptor != null ? actionDescriptor.ActionName : context.ActionDescriptor.DisplayName;

            _logger.LogDebug($"A {method} request was sent to the action \"{action}\" of controller \"{controller}\"");

            var returnContext = await next();
        }
    }
}
