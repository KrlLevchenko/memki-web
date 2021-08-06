using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Memki.Common.Validation
{
    public class ValidationBehaviour<TRequest, TResponse>: IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationBehaviour(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var validator = _serviceProvider.GetService<IValidator<TRequest>>();
            if (validator != null)
            {
                await validator.ValidateAndThrowAsync(request,  cancellationToken);
            }
            return await next();
        }
    }
}