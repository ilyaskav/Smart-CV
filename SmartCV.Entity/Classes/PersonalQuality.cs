using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCV.Entity.Classes
{
    [Table("PersonalQualities")]
    public class PersonalQuality : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int ResumeId { get; set; }


        #region navigation

        [ForeignKey("ResumeId")]
        public virtual Resume Resume { get; set; }

        #endregion

    }
}
