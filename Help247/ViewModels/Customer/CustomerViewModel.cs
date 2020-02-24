﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Help247.ViewModels.Customer
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNo { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string City { get; set; }
    }
}