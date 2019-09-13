using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCV.Entity.Classes
{
    // данные резюме
    [Table("PersonalData")]
    public class PersonalData : IEntity
    {
        [Key, ForeignKey("Resume")]
        public int Id { get; set; }

        [Required]
        public String FirstName { get; set; }

        [Required]
        public String LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        //[Required]
        public String CurrentLocation { get; set; }

        // path to img
        public String Photo { get; set; }

        [Required]
        public String Goal { get; set; }


        #region navigation

        public virtual Resume Resume { get; set; }

        #endregion

    }
}
