using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCV.Entity.Classes
{
    [Table("Resumes")]
    public class Resume : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public long UserId { get; set; }

        [Required]
        public int ProfessionId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public string Link { get; set; }

        [Required]
        public Guid Guid { get; set; }


        #region navigation

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual PersonalData PersonalData { get; set; }

        [ForeignKey("ProfessionId")]
        public virtual Profession Profession { get; set; }

        public virtual ICollection<Institution> Institutions { get; set; }

        public virtual ICollection<WorkPlace> WorkPlaces { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }

        public virtual ICollection<ResumeLanguage> ResumeLanguages { get; set; }

        public virtual ICollection<PersonalQuality> PersonalQualities { get; set; }

        public virtual ICollection<Certificate> CertificatesAndTrainings { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }

        #endregion

        public Resume()
        {
            Contacts = new List<Contact>();
        }
    }
}
