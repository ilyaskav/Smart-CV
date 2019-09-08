using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCV.Entity.Classes
{
    public class Skill : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int ResumeId { get; set; }

        [Required]
        public String Name { get; set; }


        #region navigation

        [ForeignKey("ResumeId")]
        public virtual Resume Resume { get; set; }
 
	    #endregion    
    }
}
