using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Classes
{
    // место работы
    public class WorkPlace : IEntity
    {
        [Key]
        public int Id { get; set; }
        
        public String City { get; set; }

        [Required]
        public String Name { get; set; }

        public String Description { get; set; }

        [Required]
        public String Position { get; set; }

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
        }
    }
}
