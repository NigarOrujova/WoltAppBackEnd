using System;
using System.Collections.Generic;
using System.Text;

namespace WoltBusiness.DTOs.Basket
{
    public class UserBasketDTO
    {
        public List<BasketDTO> basketDTOs { get; set; }
        public decimal TotalCount { get; set; }
    }
}
