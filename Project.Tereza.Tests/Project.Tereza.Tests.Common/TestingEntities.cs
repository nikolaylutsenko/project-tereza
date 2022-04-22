using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MDEvents.Services.Specification.SpecificationParameters;
using Project.Tereza.Core;
using Project.Tereza.Requests.Requests.Specifications;
using Project.Tereza.Responses.Responses;

namespace Project.Tereza.Tests.Project.Tereza.Tests.Common
{
    public static class TestingEntities
    {
        public static SpecificationParameters<Need> GetSpecificationParameterOfNeed => new SpecificationParameters<Need> { Skip = 0, Take = 3 };
        public static NeedSpecificationRequest GetNeedSpecificationRequest => new NeedSpecificationRequest { Skip = 0, Take = 3 };
        public static List<Need> GetListOfNeeds => new List<Need>
            {
                new("82d257a5-d72b-4f08-bcf2-76ebdc958b5f", "Laptop", "Need laptop for working needs.", 1, false),
                new("b47fdb0a-76d4-4b89-bf20-9cecfa4f4f82", "Royal Canin Sphyncx 2 kg", "I need food for my cat, please help!", 1, false),
                new("a961067e-c777-42c2-8fee-71180d750bd7", "Aspirin", "Please, I can't find this drug in retail", 3, false)
            };

        public static List<NeedResponse> GetListOfNeedResponses => new List<NeedResponse>
        {
            new(Guid.Parse("82d257a5-d72b-4f08-bcf2-76ebdc958b5f"), "Laptop", "Need laptop for working needs.", 1, false),
            new(Guid.Parse("b47fdb0a-76d4-4b89-bf20-9cecfa4f4f82"), "Royal Canin Sphyncx 2 kg", "I need food for my cat, please help!", 1, false),
            new(Guid.Parse("a961067e-c777-42c2-8fee-71180d750bd7"), "Aspirin", "Please, I can't find this drug in retail", 3, false)
        };

    }
}