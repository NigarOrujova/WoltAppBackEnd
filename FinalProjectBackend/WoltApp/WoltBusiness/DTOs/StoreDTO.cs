using System;
using System.Collections.Generic;
using System.Text;
using WoltEntity.Entities;

namespace WoltBusiness.DTOs
{
    public class StoreDTO
    {
        public Store Store { get; set; }
        public List<StoreProduct> StoreProducts { get; set; }
        public List<StoreCategory> StoreCategories { get; set; }
    }
}
