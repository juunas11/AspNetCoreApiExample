using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicsStoreApi.Hateoas
{
    public class ValueWrapper
    {
        public object Value { get; }

        public ValueWrapper(object value)
        {
            Value = value;
        }
    }
}
