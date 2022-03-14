using System;
using System.Collections.Generic;
using System.Text;

namespace WoltBusiness.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string ImageURL { get; set; }
        public decimal DiscountPercent { get; set; }
        public int StockCount { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsNew { get; set; }
        public bool Discount { get; set; }
        public int CategoryId { get; set; }
    }
}
