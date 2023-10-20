using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Sparks.Api.Pipelines;

public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly IValidator<TRequest> _validator;
    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators, IValidator<TRequest> validator)
    {
        _validators = validators;
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        //var validationResults = await Task.WhenAll(_validators.Select(validator => validator.ValidateAsync(request, cancellationToken)));
        ValidationResult? validationResults = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResults.IsValid)
        {
            throw new ValidationException(validationResults.Errors);
        }


        return await next();
    }
}