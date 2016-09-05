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
        CertificateAddModel Get(int managerId);

        void CreateCertificate(CertificateModel model);

        bool UpdateCertificate(CertificateModel model);

        void CreateOrUpdate(CertificateAddModel addModel);

        void RemoveCertificate(int id);
    }
}
