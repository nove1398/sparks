using FluentValidation;

namespace Sparks.Api.Features.Users;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => Enum.GetValues<GenderTypes>().Contains(x.Gender));
    }
}