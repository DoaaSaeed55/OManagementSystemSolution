﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.Core.Entities.Orders;
namespace OMS.Core.Entities
{
    public class Customer:BaseEntity
    {
        //public int CustomerId { get; set; }
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();


    }
}
