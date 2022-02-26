using System;
using System.Collections.Generic;
using System.Text;
using WoltEntity.Entities;

namespace WoltBusiness.DTOs
{
    public class RestaurantDTO
    {
        public Restaurant Restaurant { get; set; }
        public List<RestaurantProduct> RestaurantProducts { get; set; }
        public List<RestaurantCategory> RestaurantCategories { get; set; }
    }
}
