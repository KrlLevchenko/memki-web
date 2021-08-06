using FluentValidation;
using JetBrains.Annotations;

namespace Memki.Components.Auth.Api.Login
{
    [UsedImplicitly]
    public class RequestValidator: AbstractValidator<Request>
    {
        public RequestValidator()
        {
            RuleFor(x => x.Credentials).NotNull();
            RuleFor(x => x.Credentials).SetValidator(new CredentialsValidator());
        }   
        
        private class CredentialsValidator: AbstractValidator<CredentialsDto>
        {
            public CredentialsValidator()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }
    }
    
    
}