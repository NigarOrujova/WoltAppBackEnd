using System;
using System.Collections.Generic;
using System.Text;

namespace WoltBusiness.DTOs.Basket
{
    public class BasketDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Catagory { get; set; }
        public string ImageURL { get; set; }
        public decimal DiscountPercent { get; set; }
        public int StockCount { get; set; }
        public bool IsActive { get; set; }
    }
}
