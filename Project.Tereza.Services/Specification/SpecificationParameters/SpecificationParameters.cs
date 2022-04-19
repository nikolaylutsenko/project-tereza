using System;

namespace MDEvents.Services.Specification.SpecificationParameters
{
    public class SpecificationParameters<T> where T : class
    {
        // paging
        public int Skip { get; set; }
        public int Take { get; set; }

        // here will be sorting option


        // here will be filtering options
    }
}