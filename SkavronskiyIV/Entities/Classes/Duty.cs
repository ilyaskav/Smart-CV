﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Classes
{
    // обязанности на работе
    [Table("Duties")]
    public class Duty : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }

        public int WorkPlaceId { get; set; }

        #region navigation

        [ForeignKey("WorkPlaceId")]
        public virtual WorkPlace WorkPlace { get; set; } 

        #endregion

    }
}
