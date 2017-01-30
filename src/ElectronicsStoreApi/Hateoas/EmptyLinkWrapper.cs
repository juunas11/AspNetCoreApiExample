using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicsStoreApi.Hateoas
{
    public class EmptyLinkWrapper
    {
        public ICollection<LinkValue> Links { get; }

        public EmptyLinkWrapper()
        {
            Links = new List<LinkValue>();
        }
    }
}
