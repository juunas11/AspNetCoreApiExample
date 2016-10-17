namespace ElectronicsStoreApi.DomainModels
{
    public class OrderRow
    {
        public long Id { get; set; }
        public int? Quantity { get; set; }
        public decimal? SingleProductPrice { get; set; }

        public long? ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}