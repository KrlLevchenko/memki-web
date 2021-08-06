using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Memki.Common.ExceptionHandling
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IExceptionHandler[] _exceptionHandlers;

        public ExceptionHandlingMiddleware(RequestDelegate next, IServiceProvider serviceProvider,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _exceptionHandlers = serviceProvider.GetServices<IExceptionHandler>().ToArray();
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                foreach (var exceptionHandler in _exceptionHandlers)
                {
                    var handled = exceptionHandler.Handle(e);
                    if (handled == null) 
                        continue;
                    
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int) handled.StatusCode;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(handled.Body));
                    return;
                }
                _logger.LogError(e, "Unhandled exception occured");
                throw;
            }
        }
    }
}