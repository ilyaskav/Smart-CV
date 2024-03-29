﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Classes
{
    [Table("Certificates")]
    public class Certificate : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }

        public DateTime Date { get; set; }

        public String Location { get; set; }

        public int ResumeId { get; set; }


        #region navigation

        [ForeignKey("ResumeId")]
        public virtual Resume Resume { get; set; } 

        #endregion
    }
}
