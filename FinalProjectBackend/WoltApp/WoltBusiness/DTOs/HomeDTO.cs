using System;
using System.Collections.Generic;
using System.Text;
using WoltEntity.Entities;

namespace WoltBusiness.DTOs
{
    public class HomeDTO
    {
        public List<Category> Categories { get; set; }
        public List<Restaurant> Restaurants { get; set; }
        public List<Store> Stores { get; set; }
        public List<Slider> Sliders { get; set; }
    }
}
