using FluentValidation;
using JetBrains.Annotations;

namespace Memki.Components.Auth.Api.Register
{
    [UsedImplicitly]
    public class RequestValidator: AbstractValidator<Request>
    {
        public RequestValidator()
        {
            RuleFor(x => x.UserDto).NotNull();
            RuleFor(x => x.UserDto).SetValidator(new UserDtoValidator());
        }   
        
        private class UserDtoValidator: AbstractValidator<UserDto>
        {
            public UserDtoValidator()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }
    }
    
    
}