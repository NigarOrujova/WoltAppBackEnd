using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WoltBusiness.DTOs.Account
{
    public class ChangePasswordDTO
    {
        [Required, MaxLength(255), DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [Required, MaxLength(255), DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password), Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
