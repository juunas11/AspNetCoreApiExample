using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicsStoreApi.DomainModels
{
    public class OrderRow
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public int? Quantity { get; set; }
        [Required]
        public decimal? SingleProductPrice { get; set; }

        [Required]
        [ForeignKey("Product")]
        public long? ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        [ForeignKey("Order")]
        public long? OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}