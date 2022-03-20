using System;
using System.Collections.Generic;
using System.Text;

namespace WoltEntity.Entities
{
    public class FullOrder
    {
        public int Id { get; set; }
        public double TotalCount { get; set; }
        public string Adress { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public List<Order> Orders { get; set; }
    }
}
