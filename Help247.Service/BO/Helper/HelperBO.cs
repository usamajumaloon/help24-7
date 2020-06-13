﻿using Help247.Common.Utility;
using Help247.Service.BO.HelperCategory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Help247.Service.BO.Helper
{
    public class HelperBO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string AddressLine { get; set; }
        public int Experience { get; set; }
        public string ProfilePic { get; set; }
        public string AboutMe { get; set; }
        public string MyService { get; set; }
        public HelperCategoryBO HelperCategory { get; set; }
    }
}
