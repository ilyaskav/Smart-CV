using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCV.Entity.Classes
{
    public class ResumeManager:IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public long UserId { get; set; }

        [Required]
        public int ProfessionId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public string Link { get; set; }

        [Required]
        public Guid Guid { get; set; }

        #region navigation

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual Resume Resume { get; set; }

        [ForeignKey("ProfessionId")]
        public virtual Profession Profession { get; set; }

        #endregion
    }
}
