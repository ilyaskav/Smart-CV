using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Classes
{
    public class Contact : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int ContactTitleId { get; set; }

        [Required]
        public String Data { get; set; }

        public int ResumeId { get; set; }


        #region navigation

        [ForeignKey("ResumeId")]
        public Resume Resume { get; set; }

        [ForeignKey("ContactTitleId")]
        public ContactTitle ContactTitle { get; set; }

        #endregion
    }
}
