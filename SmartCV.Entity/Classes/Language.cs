using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCV.Entity.Classes
{
    [Table ("Languages")]
    public class Language : IEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public String Level { get; set; }

        #region navigation

        //[ForeignKey("ResumeId")]
        public virtual ICollection<ResumeLanguage> Resumes { get; set; }

        #endregion
    }
}
