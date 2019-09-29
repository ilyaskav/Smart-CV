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
        public string Name { get; set; }

        [Required]
        public string Level { get; set; }

        #region navigation

        public virtual ICollection<ResumeLanguage> Resumes { get; set; }

        #endregion
    }
}
