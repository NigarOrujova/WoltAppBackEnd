using System;
using System.Collections.Generic;
using System.Text;
using WoltEntity.Entities;

namespace WoltBusiness.DTOs
{
    public class RestaurantDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsNew { get; set; }
        public bool Discount { get; set; }
        public string DiscountPercent { get; set; }
        public string ImageURL { get; set; }
        public string HeroImageURL { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public List<Comment> Comments { get; set; }
        public List<RestaurantProduct> RestaurantProducts { get; set; }
        public List<RestaurantCategory> RestaurantCategories { get; set; }

    }
}
