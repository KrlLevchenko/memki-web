using System;
using System.Net;
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
                    Body = "Validation message",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            return null;
        }
    }
}