﻿using System;
using System.Collections.Generic;
using System.Text;
using WoltEntity.Entities;

namespace WoltBusiness.DTOs
{
    public class DashboardDTO:HomeDTO
    {
        public List<Product> Products { get; set; }
    }
}
