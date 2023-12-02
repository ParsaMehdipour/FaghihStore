using FluentResults;

using FluentValidation;

using MediatR;

namespace SH.Application.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : ResultBase<TResponse>, new()
{
    protected IEnumerable<IValidator<TRequest>> _validators { get; }

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(_ => _.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .Where(_ => _.Errors.Any())
                .SelectMany(_ => _.Errors)
                .ToList();

            TResponse result = new();

            foreach (var failure in failures)
                result.Reasons.Add(new Error(failure.ErrorMessage));

            if (result.Errors.Any())
                return result;
        }

        return await next();
    }
}