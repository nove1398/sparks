using FluentValidation;

namespace Sparks.Api.Features.Users;

public class UserValidator : AbstractValidator<CreateUserCommand>
{
    public UserValidator()
    {
      
    }
}