using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartCV.Service.Models
{
    public class CertificateAddModel
    {
        public int? ResumeManagerId { get; set; }

        [UIHint("Certificates")]
        public ICollection<CertificateModel> Certificates { get; set; }

        public CertificateAddModel()
        {
            Certificates = new List<CertificateModel>();
        }
    }
}
