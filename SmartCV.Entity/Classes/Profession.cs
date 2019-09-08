using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCV.Entity.Classes
{
    public class Profession:IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Rules { get; set; }

        #region navigation

        public ICollection<ResumeManager> ResumeManagers { get; set; }

        #endregion

        public Profession()
        {
            ResumeManagers = new List<ResumeManager>();
        }
    }
}
