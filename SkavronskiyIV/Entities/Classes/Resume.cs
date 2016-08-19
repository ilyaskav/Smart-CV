using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Classes
{
    // данные резюме
    [Table("Resumes")]
    public class Resume : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String FirstName { get; set; }

        [Required]
        public String LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public String CurrentLocation { get; set; }

        // path to img
        public String Photo { get; set; }

        [Required]
        public String Goal { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }


        #region navigation

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Institution> Education { get; set; }

        public virtual ICollection<WorkPlace> WorkExp { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }

        public virtual ICollection<Language> Languages { get; set; }

        public virtual ICollection<PersonalQuality> PersonalQualities { get; set; }

        public virtual ICollection<Certificate> CertificatesAndTrainings { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }

        #endregion


        public Resume()
        {
            Education = new List<Institution>();
            WorkExp = new List<WorkPlace>();
            Skills = new List<Skill>();
            Languages = new List<Language>();
            PersonalQualities = new List<PersonalQuality>();
            CertificatesAndTrainings = new List<Certificate>();
            Contacts = new List<Contact>();
        }
    }
}
