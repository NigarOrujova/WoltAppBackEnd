using System;
using System.Collections.Generic;
using System.Text;

namespace WoltEntity.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public bool IsDeleted { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int FullOrderId { get; set; }
        public FullOrder FullOrder { get; set; }
    }
}
