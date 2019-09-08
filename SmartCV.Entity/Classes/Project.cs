using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCV.Entity.Classes
{
    public class Project : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String Title { get; set; }

        //[Required]
        public String Duration { get; set; }

        [Required]
        public String Description { get; set; }

        [Required]
        public String Role { get; set; }

        [Required]
        public String TechStack { get; set; }

        public String About { get; set; }

        public int WorkPlaceId { get; set; }


        #region navigation

        [ForeignKey("WorkPlaceId")]
        public WorkPlace WorkPlace { get; set; }

        #endregion
    }
}
