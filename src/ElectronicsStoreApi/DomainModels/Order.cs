using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElectronicsStoreApi.DomainModels
{
    public class Order
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerAddress { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public virtual ICollection<OrderRow> Rows { get; set; }

        public Order()
        {
        }
    }
}
