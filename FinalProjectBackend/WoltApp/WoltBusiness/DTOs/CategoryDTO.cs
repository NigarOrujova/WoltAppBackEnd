using System;
using System.Collections.Generic;
using System.Text;
using WoltEntity.Entities;

namespace WoltBusiness.DTOs
{
    public class CategoryDTO
    {
        public List<Restaurant> Restaurants { get; set; }
        public List<RestaurantProduct> RestaurantProducts { get; set; }
        public List<RestaurantCategory> RestaurantCategories { get; set; }
    }
}
