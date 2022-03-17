using System;
using System.Collections.Generic;
using System.Text;

namespace WoltEntity.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public string UserName { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

    }
}
