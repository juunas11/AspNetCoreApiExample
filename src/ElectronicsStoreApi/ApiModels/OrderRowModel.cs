using System.ComponentModel.DataAnnotations;

namespace ElectronicsStoreApi.ApiModels
{
    public class OrderRowModel
    {
        public long Id { get; set; }
        [Required]
        public int? Quantity { get; set; }
        [Required]
        public decimal? SingleProductPrice { get; set; }
        [Required]
        public long? ProductId { get; set; }
    }
}
