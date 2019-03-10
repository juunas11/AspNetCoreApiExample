using System.ComponentModel.DataAnnotations;

namespace ElectronicsStoreApi.ApiModels
{
    public class OrderModel
    {
        public long Id { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerAddress { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
    }
}
