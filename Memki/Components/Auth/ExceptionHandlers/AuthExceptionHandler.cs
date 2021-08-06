using System;
using System.Net;
using JetBrains.Annotations;
using Memki.Common.ExceptionHandling;
using Memki.Model.Auth.Exceptions;

namespace Memki.Components.Auth.ExceptionHandlers
{
    [UsedImplicitly]
    public class AuthExceptionHandler: IExceptionHandler
    {
        public HandleResult? Handle(Exception exception)
        {
            if (exception is UserDuplicateException)
            {
                return new HandleResult()
                {
                    Body = "user_already_exist",
                    StatusCode = HttpStatusCode.Conflict
                };
            }
            return null;
        }
    }
}