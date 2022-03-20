using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace WoltEntity.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public string ImageURL { get; set; }
        public IFormFile Photo { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public List<int> RestaurantIds { get; set; } = new List<int>();
        public List<int> StoreIds { get; set; } = new List<int>();
        public List<Product> Products { get; set; }
        public List<RestaurantCategory> RestaurantCategories { get; set; }
        public List<StoreCategory> StoreCategories { get; set; }
    }
}
