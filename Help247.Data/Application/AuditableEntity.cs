﻿using Help247.Common.Utility;
using Help247.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Help247.Data.Application
{
    public class AuditableEntity : IAuditableEntity
    {
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public int CreatedById { get; set; }
        public DateTime? EditedOn { get; set; }
        public int? EditedById { get; set; }
        public Enums.RecordStatus RecordState { get; set; } = Enums.RecordStatus.Active;


        protected AuditableEntity()
        {

        }

        protected AuditableEntity(User user)
        {
                CreatedOn = DateTime.UtcNow;
                CreatedById = Int32.Parse(user.Id);
                EditedOn = DateTime.UtcNow;
                RecordState = Enums.RecordStatus.Active;
            
        }
    }
}
