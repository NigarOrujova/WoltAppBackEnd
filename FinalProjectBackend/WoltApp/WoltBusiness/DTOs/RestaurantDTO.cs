using System;
using System.Collections.Generic;
using System.Text;
using WoltEntity.Entities;

namespace WoltBusiness.DTOs
{
    public class RestaurantDTO
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public Restaurant Restaurant { get; set; }

    }
}
