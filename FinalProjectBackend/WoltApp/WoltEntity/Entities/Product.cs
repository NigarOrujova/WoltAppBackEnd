﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace WoltEntity.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public int BasketCount { get; set; }
        public string ImageURL { get; set; }
        public IFormFile Photo { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsNew { get; set; }
        public bool Discount { get; set; }
        public decimal DiscountPercent { get; set; }
        public DateTime IsActive { get; set; } = DateTime.UtcNow.AddHours(4);
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<int> RestaurantIds { get; set; } = new List<int>();
        public List<int> StoreIds { get; set; } = new List<int>();
        public List<Order> Orders { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public List<RestaurantProduct> RestaurantProducts { get; set; }
        public List<StoreProduct> StoreProducts { get; set; }
    }
}
