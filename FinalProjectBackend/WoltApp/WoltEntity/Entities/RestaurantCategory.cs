using System;
using System.Collections.Generic;
using System.Text;

namespace WoltEntity.Entities
{
    public class RestaurantCategory
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public Category Category { get; set; }
    }
}
