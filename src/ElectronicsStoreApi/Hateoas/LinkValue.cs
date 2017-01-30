namespace ElectronicsStoreApi.Hateoas
{
    public struct LinkValue
    {
        public string Rel { get; }
        public string Href { get; }

        public LinkValue(string rel, string href)
        {
            Rel = rel;
            Href = href;
        }
    }
}