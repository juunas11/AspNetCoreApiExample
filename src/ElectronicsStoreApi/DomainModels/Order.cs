using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicsStoreApi.DomainModels
{
    public class Order
    {
        public long Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerEmail { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public virtual ICollection<OrderRow> Rows { get; set; }

        public Order()
        {
            Rows = new List<OrderRow>();
        }
    }
}
