using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartCV.Service.Models
{
    public class InstitutionAddModel
    {
        public int ResumeManagerId { get; set; }

        [UIHint("Institutions")]
        public ICollection<InstitutionModel> Institutions { get; set; }

        public InstitutionAddModel()
        {
            Institutions = new List<InstitutionModel>();
        }
    }
}
