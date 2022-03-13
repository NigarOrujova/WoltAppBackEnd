using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace WoltBusiness.DTOs
{
    public class MultipleSliderDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<IFormFile> Photos { get; set; }
    }
}
