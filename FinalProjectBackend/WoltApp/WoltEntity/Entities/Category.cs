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
        public ICollection<Product> Products { get; set; }
        public ICollection<RestaurantCategory> RestaurantCategories { get; set; }
        public ICollection<StoreCategory> StoreCategories { get; set; }
    }
}
