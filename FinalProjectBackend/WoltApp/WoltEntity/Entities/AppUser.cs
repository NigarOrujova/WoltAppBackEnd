using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WoltEntity.Entities
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActivated { get; set; }
        public List<BasketItem> BasketItems { get; set; }
    }
}
