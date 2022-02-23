using System;
using System.Collections.Generic;
using System.Text;

namespace WoltEntity.Entities
{
    public class RestaurantProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public Product Product { get; set; }
    }
}
