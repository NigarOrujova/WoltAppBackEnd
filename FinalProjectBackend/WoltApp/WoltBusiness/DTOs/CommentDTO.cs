using System;
using System.Collections.Generic;
using System.Text;

namespace WoltBusiness.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public string UserName { get; set; }
        public string Content { get; set; }
    }
}
