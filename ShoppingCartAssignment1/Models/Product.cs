﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingCartAssignment1.Models
{
    public class Product
    {
        [Key]
        public Guid PId { get; set; } = Guid.NewGuid();
        public string PName { get; set; }
        public string PDescription { get; set; }
        public string PPrice { get; set; }
        public string PImage { get; set; }

        public ICollection<Order> Orders { get; set;}
    }
}