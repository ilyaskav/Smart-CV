using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCV.Entity.Classes
{
    // обязанности на работе
    [Table("Duties")]
    public class Duty : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int WorkPlaceId { get; set; }

        #region navigation

        [ForeignKey("WorkPlaceId")]
        public virtual WorkPlace WorkPlace { get; set; } 

        #endregion

    }
}
