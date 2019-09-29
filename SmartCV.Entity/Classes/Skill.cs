using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCV.Entity.Classes
{
    public class Skill : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int ResumeId { get; set; }

        [Required]
        public string Name { get; set; }


        #region navigation

        [ForeignKey("ResumeId")]
        public virtual Resume Resume { get; set; }
 
	    #endregion    
    }
}
