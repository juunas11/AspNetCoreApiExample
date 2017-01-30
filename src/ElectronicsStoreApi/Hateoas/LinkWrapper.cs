using System.Collections.Generic;

namespace ElectronicsStoreApi.Hateoas
{
    public class LinkWrapper
    {
        public object Value { get; }
        public ICollection<LinkValue> Links { get; }

        public LinkWrapper(object value)
        {
            Value = value;
        }
    }
}