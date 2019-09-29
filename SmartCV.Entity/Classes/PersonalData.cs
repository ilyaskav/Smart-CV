using System;
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
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        //[Required]
        public string CurrentLocation { get; set; }

        // path to img
        public string Photo { get; set; }

        [Required]
        public string Goal { get; set; }


        #region navigation

        public virtual Resume Resume { get; set; }

        #endregion

    }
}
