using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Sparks.Api.Shared;

namespace Sparks.Api.Pipelines;

public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>>? _validators;
    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>>? validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators is null || !_validators.Any())
        {
            return await next();
        }
        
        var validationFailures = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure != null)
            .ToList();

        if (validationFailures.Any())
        {
            throw new ValidationException("validationFailures");
        }



        return await next();
    }
}