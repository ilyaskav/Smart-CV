using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
