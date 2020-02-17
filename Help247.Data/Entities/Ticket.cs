﻿using Help247.Common.Utility;
using Help247.Data.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Help247.Data.Entities
{
    [Table("Tickets", Schema = "Help247")]
    public class Ticket : AuditableEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Enums.TicketStatus Status { get; set; } = Enums.TicketStatus.None;

        #region foreign key
        public int HelperId { get; set; }
        public Helper Helper { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        #endregion
    }
}
