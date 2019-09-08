using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCV.Entity.Classes
{
    [Table("ContactTitles")]
    public class ContactTitle: IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String Title { get; set; }

        
        #region navigation

        public virtual ICollection<Contact> Contacts { get; set; }

        #endregion


        public ContactTitle()
        {
            Contacts = new List<Contact>();
        }
    }
}
