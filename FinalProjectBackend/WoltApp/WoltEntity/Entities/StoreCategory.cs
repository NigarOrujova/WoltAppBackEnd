using System;
using System.Collections.Generic;
using System.Text;

namespace WoltEntity.Entities
{
    public class StoreCategory
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
    }
}
