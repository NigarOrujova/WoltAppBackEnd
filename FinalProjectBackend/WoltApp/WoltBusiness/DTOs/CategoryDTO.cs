using System;
using System.Collections.Generic;
using System.Text;
using WoltEntity.Entities;

namespace WoltBusiness.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public string ImageURL { get; set; }
        public List<Restaurant> Restaurants { get; set; }
        public List<RestaurantProduct> RestaurantProducts { get; set; }
        public List<RestaurantCategory> RestaurantCategories { get; set; }
    }
}
