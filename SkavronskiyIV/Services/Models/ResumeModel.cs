using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class ResumeModel
    {
        public int? Id { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public String CurrentLocation { get; set; }

        // path to img
        public String Photo { get; set; }

        public String Goal { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; }



        //public virtual ICollection<Institution> Education { get; set; }

        //public virtual ICollection<WorkPlace> WorkExp { get; set; }

        //public virtual ICollection<Skill> Skills { get; set; }

        //public virtual ICollection<Language> Languages { get; set; }

        //public virtual ICollection<PersonalQuality> PersonalQualities { get; set; }

        //public virtual ICollection<Certificate> CertificatesAndTrainings { get; set; }

        //public virtual ICollection<ContactModel> Contacts { get; set; }


    }
}
