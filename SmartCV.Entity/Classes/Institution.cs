using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCV.Entity.Classes
{
    // учебное заведение
    public class Institution : IEntity
    {
        [Key]
        public int Id { get; set; }        

        public string City { get; set; }

        [Required]
        public string Name { get; set; }

        public string Department { get; set; }

        [Required]
        public string Specialty { get; set; }

        [Required]
        public string Degree { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime From { get; set; }

        [Column(TypeName = "date")]
        public DateTime? To { get; set; }

        public int ResumeId { get; set; }


        #region navigation

        [ForeignKey("ResumeId")]
        public virtual Resume Resume { get; set; } 

        #endregion
    }
}
