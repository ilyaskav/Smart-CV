using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Classes
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

        //public int ResumeId { get; set; }


        #region navigation

        //[ForeignKey("ResumeId")]
        public virtual ICollection<Resume> Resumes { get; set; }

        #endregion
    }
}
