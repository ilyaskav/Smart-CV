using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

 
namespace Services.Models
{
    public class ResumeModel
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength=3)]
        [DisplayName("Имя")]
        public String FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [DisplayName("Фамилия")]
        public String LastName { get; set; }

        //[Required]
        //[DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Дата рождения")]
        public DateTime DateOfBirth { get; set; }

        public String CurrentLocation { get; set; }

        // path to img
        [StringLength(100, MinimumLength = 8)]
        public String Photo { get; set; }

        [Required]
        [StringLength(100, MinimumLength=10)]
        [DisplayName("Цель")]
        public String Goal { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        //[Required]
        public int UserId { get; set; }


        //public ICollection<InstitutionModel> Education { get; set; }

        //public ICollection<WorkPlaceModel>  WorkExp { get; set; }

        //public ICollection<SkillModel> Skills { get; set; }

        //public ICollection<LanguageModel> Languages { get; set; }

        //public ICollection<PersonalQualityModel> PersonalQualities { get; set; }

        //public ICollection<CertificateModel> Ceftificates { get; set; }

        //public ICollection<ContactModel> Contacts { get; set; }

        public ResumeModel()
        {
            CreatedAt = DateTime.Now;
        //    Education = new List<InstitutionModel>();
        //    WorkExp = new List<WorkPlaceModel>();
        //    Skills = new List<SkillModel>();
        //    Languages = new List<LanguageModel>();
        //    PersonalQualities = new List<PersonalQualityModel>();
        //    Ceftificates = new List<CertificateModel>();
        //    Contacts = new List<ContactModel>();
        }

    }
}
