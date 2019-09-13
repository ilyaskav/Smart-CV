using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartCV.Entity.Classes
{
    public class Profession : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Rules { get; set; }

        #region navigation

        public ICollection<Resume> Resumes { get; set; }

        #endregion

        public Profession()
        {
            Resumes = new List<Resume>();
        }
    }
}
