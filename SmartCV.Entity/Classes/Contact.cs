using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCV.Entity.Classes
{
    public class Contact : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int ContactTitleId { get; set; }

        [Required]
        public string Data { get; set; }

        public int ResumeId { get; set; }


        #region navigation

        [ForeignKey("ResumeId")]
        public virtual Resume Resume { get; set; }

        [ForeignKey("ContactTitleId")]
        public virtual ContactTitle ContactTitle { get; set; }

        #endregion
    }
}
