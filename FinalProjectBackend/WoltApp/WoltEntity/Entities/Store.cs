using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace WoltEntity.Entities
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ControllerName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime IsActive { get; set; } = DateTime.UtcNow.AddHours(4);
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public string HeroImageURL { get; set; }
        public IFormFile HeroPhoto { get; set; }
        public string ImageURL { get; set; }
        public IFormFile Photo { get; set; }
        public ICollection<StoreProduct> StoreProducts { get; set; }
        public ICollection<StoreCategory> StoreCategories { get; set; }
    }
}
