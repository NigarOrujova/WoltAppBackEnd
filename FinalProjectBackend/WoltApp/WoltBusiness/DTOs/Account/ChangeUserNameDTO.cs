using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WoltBusiness.DTOs.Account
{
    public class ChangeUserNameDTO
    {
        [Required,MaxLength(50)]
        public string NewUsername { get; set; }
        [Required, MaxLength(255), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
