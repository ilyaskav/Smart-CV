using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCV.Entity.Classes
{
    public class Project : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        //[Required]
        public string Duration { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public string TechStack { get; set; }

        public string About { get; set; }

        public int WorkPlaceId { get; set; }


        #region navigation

        [ForeignKey("WorkPlaceId")]
        public WorkPlace WorkPlace { get; set; }

        #endregion
    }
}
