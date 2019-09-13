using SmartCV.Entity.Classes;
using SmartCV.Repository.Interfaces;
using SmartCV.Service.Converters;
using SmartCV.Service.Interfaces;
using SmartCV.Service.Models;
using System.Collections.Generic;

namespace SmartCV.Service.Classes
{
    public class CertificateService : ICertificateService
    {
        #region Declarations

        readonly ICertificateRepository _certificateRepository = null;
        readonly IResumeRepository _resumeRepository = null;

        #endregion

        public CertificateService(ICertificateRepository certRepo, IResumeRepository resumeRepo)
        {
            _certificateRepository = certRepo;
            _resumeRepository = resumeRepo;
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
            _resumeRepository.Dispose();
        }

        public CertificateAddModel Get(int managerId)
        {
            var resume = _resumeRepository.Get(managerId);
            if (resume == null || resume.CertificatesAndTrainings.Count == 0) return null;

            CertificateAddModel addModel = new CertificateAddModel
            {
                ResumeManagerId = managerId
            };
            foreach (var certificate in resume.CertificatesAndTrainings)
            {
                addModel.Certificates.Add(certificate.ToModel());
            }

            return addModel;
        }

        public void CreateOrUpdate(CertificateAddModel addModel)
        {
            var resume = _resumeRepository.Get(addModel.ResumeManagerId.Value);
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
