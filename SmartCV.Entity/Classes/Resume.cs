using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCV.Entity.Classes
{
    // данные резюме
    [Table("Resumes")]
    public class Resume : IEntity
    {
        [Key, ForeignKey("ResumeManager")]
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

        public virtual ICollection<Institution> Education { get; set; }

        public virtual ICollection<WorkPlace> WorkExp { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }

        public virtual ICollection<ResumeLanguage> ResumeLanguages { get; set; }

        public virtual ICollection<PersonalQuality> PersonalQualities { get; set; }

        public virtual ICollection<Certificate> CertificatesAndTrainings { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }

        public virtual ResumeManager ResumeManager { get; set; }

        #endregion


        public Resume()
        {
            Education = new List<Institution>();
            WorkExp = new List<WorkPlace>();
            Skills = new List<Skill>();
            ResumeLanguages = new List<ResumeLanguage>();
            PersonalQualities = new List<PersonalQuality>();
            CertificatesAndTrainings = new List<Certificate>();
            Contacts = new List<Contact>();
        }
    }
}
