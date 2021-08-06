using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using FluentValidation;
using JetBrains.Annotations;
using Memki.Common.ExceptionHandling;

namespace Memki.Common.Validation
{
    [UsedImplicitly]
    public class ValidationExceptionHandler: IExceptionHandler
    {
        public HandleResult? Handle(Exception exception)
        {
            if (exception is ValidationException validationException)
            {
                return new HandleResult
                {
                    Errors = validationException.Errors.Select(e => e.ErrorMessage).ToArray(),
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            return null;
        }
    }
}