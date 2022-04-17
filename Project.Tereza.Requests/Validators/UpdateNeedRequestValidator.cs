using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Project.Tereza.Requests.Requests;

namespace Project.Tereza.Requests.Validators
{
    public class UpdateNeedRequestValidator : AbstractValidator<UpdateNeedRequest>
    {
        public UpdateNeedRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(1, 1000)
                .Matches(@"^[a-zA-Z0-9\s.,%()]+$");
            RuleFor(x => x.Description)
                .NotEmpty()
                .Length(1, 10000)
                .Matches(@"^[a-zA-Z0-9\s.,%()]+$");
            RuleFor(x => x.Count)
                .NotEmpty()
                .InclusiveBetween(1, 1000000);
        }
    }
}