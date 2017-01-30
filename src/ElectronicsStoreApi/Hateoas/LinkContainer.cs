using System.Collections.Generic;

namespace ElectronicsStoreApi.Hateoas
{
    public abstract class LinkContainer
    {
        public ICollection<LinkValue> Links { get; }

        public LinkContainer()
        {
            Links = new List<LinkValue>();
        }
    }
}