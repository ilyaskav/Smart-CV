using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCV.Entity.Classes
{
    public class WorkPlace : IEntity
    {
        [Key]
        public int Id { get; set; }
        
        public string City { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public DateTime From { get; set; }

        public DateTime? To { get; set; }

        public int ResumeId { get; set; }


        #region navigation

        [ForeignKey("ResumeId")]
        public virtual Resume Resume { get; set; }

        public virtual ICollection<Duty> Duties { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

        #endregion


        public WorkPlace()
        {
            Duties = new List<Duty>();
            Projects = new List<Project>();
        }
    }
}
