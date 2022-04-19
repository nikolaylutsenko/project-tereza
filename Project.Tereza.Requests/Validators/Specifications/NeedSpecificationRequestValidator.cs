using FluentValidation;
using Project.Tereza.Requests.Requests.Specifications;

namespace Project.Tereza.Requests.Validators.Specifications;

public class NeedSpecificationRequestValidator : AbstractValidator<NeedSpecificationRequest>
{
    public NeedSpecificationRequestValidator()
    {
        RuleFor(x => x.Skip)
            .GreaterThanOrEqualTo(0);
        RuleFor(x => x.Take)
            .GreaterThanOrEqualTo(1);
    }
}
