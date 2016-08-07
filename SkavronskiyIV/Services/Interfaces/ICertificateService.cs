using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICertificateService: IDisposable
    {
        void AddCertificate(CertificateModel model);
        void UpdateCertificate(CertificateModel model);
        void RemoveCertificate(int id);
    }
}
