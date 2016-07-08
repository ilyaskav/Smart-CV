using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Classes
{
    // учебное заведение
    public class Institution : IEntity
    {
        [Key]
        public int Id { get; set; }        

        public String City { get; set; }

        [Required]
        public String Name { get; set; }

        public String Department { get; set; }

        [Required]
        public String Specialty { get; set; }

        [Required]
        public String Degree { get; set; }

        [Required]
        public DateTime From { get; set; }

        public DateTime? To { get; set; }

        public int ResumeId { get; set; }


        #region navigation

        [ForeignKey("ResumeId")]
        public virtual Resume Resume { get; set; } 

        #endregion
    }
}
