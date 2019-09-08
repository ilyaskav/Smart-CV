using SmartCV.Service.Models;
using System;

namespace SmartCV.Service.Interfaces
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
