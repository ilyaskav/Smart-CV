using Repository.Interfaces;
using Services.Interfaces;
using Services.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Classes
{
    public class CertificateService:ICertificateService
    {
        #region Declarations

        ICertificateRepository _certificateRepository = null;

        #endregion

        public CertificateService(ICertificateRepository certRepo)
        {
            _certificateRepository = certRepo;
        }


        public void AddCertificate(Models.CertificateModel model)
        {
            _certificateRepository.Add(model.ToEntity());
        }

        public void UpdateCertificate(Models.CertificateModel model)
        {
            if (model.Id == null) return;
            if (_certificateRepository.Has(model.Id.Value))
            {
                var entity = model.ToEntity();
                _certificateRepository.Update(entity);
            }
        }

        public void RemoveCertificate(int id)
        {
            if (_certificateRepository.Has(id)) _certificateRepository.Remove(id);
        }

        public void Dispose()
        {
            _certificateRepository.Dispose();
        }
    }
}
