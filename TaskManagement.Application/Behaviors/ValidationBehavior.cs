using FluentValidation;
using MediatR;

namespace TaskManagement.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        // Call ValidateAsync for asynchronous validation
        var validationFailures = (await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken))
            ))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();

        if (validationFailures.Count != 0)
            throw new ValidationException(validationFailures);

        return await next();
    }
}