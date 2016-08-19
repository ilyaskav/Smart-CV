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

        public int UserId { get; set; }


        //public ICollection<InstitutionModel> Education { get; set; }

        //public ICollection<WorkPlaceModel>  WorkExp { get; set; }

        //public ICollection<SkillModel> Skills { get; set; }

        //public ICollection<LanguageModel> Languages { get; set; }

        //public ICollection<PersonalQualityModel> PersonalQualities { get; set; }

        //public ICollection<CertificateModel> Ceftificates { get; set; }

        //public ICollection<ContactModel> Contacts { get; set; }

        //public ResumeModel()
        //{
        //    Education = new List<InstitutionModel>();
        //    WorkExp = new List<WorkPlaceModel>();
        //    Skills = new List<SkillModel>();
        //    Languages = new List<LanguageModel>();
        //    PersonalQualities = new List<PersonalQualityModel>();
        //    Ceftificates = new List<CertificateModel>();
        //    Contacts = new List<ContactModel>();
        //}

    }
}
