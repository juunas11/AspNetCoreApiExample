using System.ComponentModel.DataAnnotations;

namespace ElectronicsStoreApi.ApiModels
{
    public class ProductModel
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public decimal? Price { get; set; }
    }
}
