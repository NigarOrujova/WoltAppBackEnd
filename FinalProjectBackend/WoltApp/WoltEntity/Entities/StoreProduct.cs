using System;
using System.Collections.Generic;
using System.Text;

namespace WoltEntity.Entities
{
    public class StoreProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
    }
}
