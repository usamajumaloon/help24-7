﻿using Help247.Common.Utility;
using Help247.Service.BO.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Help247.Service.BO.Ticket
{
    public class TicketBO
    {
        public int Id { get; set; }
        public DateTime HelpDateFrom { get; set; }
        public DateTime HelpDateTo { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ContactNo1 { get; set; }
        public string ContactNo2 { get; set; }
        public string OtherRequirements { get; set; }
        public string HelpTime { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }



        public int TicketStatusId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedById { get; set; }
        public int CustomerId { get; set; }
        public HelperBO Helper { get; set; }


    }
}
