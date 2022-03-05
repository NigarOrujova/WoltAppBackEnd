using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WoltBusiness.DTOs.Account
{
    public class ChangeEmailDTO
    {
        [Required, MaxLength(255), DataType(DataType.EmailAddress)]
        public string NewEmail { get; set; }
        [Required, MaxLength(255), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
