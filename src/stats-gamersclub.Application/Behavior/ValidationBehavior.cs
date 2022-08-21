﻿using FluentValidation;
using MediatR;
using stats_gamersclub.Domain.Comum.Extensions;
using stats_gamersclub.Domain.Comum.Results;

namespace stats_gamersclub.Application.Behavior {
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : class, IResult {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) {
            var failures = _validators.Select(v => v.Validate(new ValidationContext<TRequest>(request)))
                                      .SelectMany(result => result.AsErrors())
                                      .Where(f => f != null)
                                      .ToList();

            return failures.Any() ? Errors(failures) : next();
        }

        private static Task<TResponse> Errors(List<ValidationError> failures) {
            var response = Result.BadRequest(failures) as TResponse ?? throw new NullReferenceException("Um erro ocorreu na validação do modelo e não foi possível criar a resposta.");

            return Task.FromResult(response);
        }
    }
}
