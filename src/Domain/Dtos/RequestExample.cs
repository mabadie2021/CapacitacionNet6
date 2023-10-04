using FluentValidation;

namespace ExampleApi.Domain.Dtos;

public class RequestExample
{
      public string Name { get; set; }
      public string Description { get; set; }
}

public class RequestExampleValidator : AbstractValidator<RequestExample>
{
      public RequestExampleValidator()
      {
            _ = RuleFor(request => request.Name).NotEmpty().NotNull()
                    .WithMessage(errorMessage: "El Name no puede estar vacio.");
            _ = RuleFor(request => request.Name).MaximumLength(50)
                    .WithMessage(errorMessage: "El Name supera los 50 caracteres.");
            _ = RuleFor(request => request.Description).MaximumLength(500)
                    .WithMessage(errorMessage: "El Description supera los 500 caracteres.");
      }
}