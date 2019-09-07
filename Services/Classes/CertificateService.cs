using Repository.Interfaces;
using Services.Interfaces;
using Services.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Models;
using Entities.Classes;

namespace Services.Classes
{
    public class CertificateService:ICertificateService
    {
        #region Declarations

        ICertificateRepository _certificateRepository = null;
        IResumeManagerRepository _managerRepository = null;

        #endregion

        public CertificateService(ICertificateRepository certRepo, IResumeManagerRepository managerRepo)
        {
            _certificateRepository = certRepo;
            _managerRepository = managerRepo;
        }

        public void CreateCertificate(CertificateModel model)
        {
            _certificateRepository.Add(model.ToEntity());
        }

        public bool UpdateCertificate(CertificateModel model)
        {
            if (model.Id == null) return false;
            if (_certificateRepository.Has(model.Id.Value))
            {
                Certificate entity = model.ToEntity();
                return _certificateRepository.Update(entity);
            }
            return false;
        }

        public void RemoveCertificate(int id)
        {
            if (_certificateRepository.Has(id)) _certificateRepository.Remove(id);
        }

        public void Dispose()
        {
            _certificateRepository.Dispose();
            _managerRepository.Dispose();
        }

        public CertificateAddModel Get(int managerId)
        {
            var resume = _managerRepository.Get(managerId).Resume;
            if (resume == null || resume.CertificatesAndTrainings.Count == 0) return null;

            CertificateAddModel addModel = new CertificateAddModel();
            addModel.ResumeManagerId = managerId;
            foreach (var certificate in resume.CertificatesAndTrainings)
            {
                addModel.Certificates.Add(certificate.ToModel());
            }

            return addModel;
        }

        public void CreateOrUpdate(CertificateAddModel addModel)
        {
            var resume = _managerRepository.Get(addModel.ResumeManagerId.Value).Resume;
            foreach (var certificate in addModel.Certificates)
            {
                certificate.ResumeId = resume.Id;
                if (!this.UpdateCertificate(certificate))
                {
                    this.CreateCertificate(certificate);
                }
            }
        }
    }
}
