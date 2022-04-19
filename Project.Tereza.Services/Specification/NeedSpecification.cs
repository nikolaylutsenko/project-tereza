using MDEvents.Services.Specification.SpecificationParameters;
using Project.Tereza.Core;
using Project.Tereza.Services.Helpers;

namespace MDEvents.Services.Specification
{
    public class NeedSpecification : BaseSpecification<Need>
    {
        public NeedSpecification(SpecificationParameters<Need> parameters)
        {
            Skip = parameters.Skip;
            Take = parameters.Take;

            // create filter list
            var predicate = PredicateBuilder.True<Need>();

            // here apply filtering options


            // set filtering
            Criteria = predicate;
        }
    }
}