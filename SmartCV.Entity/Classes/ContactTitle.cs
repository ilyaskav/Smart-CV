using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCV.Entity.Classes
{
    [Table("ContactTitles")]
    public class ContactTitle: IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        
        #region navigation

        public virtual ICollection<Contact> Contacts { get; set; }

        #endregion


        public ContactTitle()
        {
            Contacts = new List<Contact>();
        }
    }
}
