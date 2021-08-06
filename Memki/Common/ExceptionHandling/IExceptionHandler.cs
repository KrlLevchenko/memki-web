using System;

namespace Memki.Common.ExceptionHandling
{
    public interface IExceptionHandler
    {
        HandleResult? Handle(Exception exception);
    }
}