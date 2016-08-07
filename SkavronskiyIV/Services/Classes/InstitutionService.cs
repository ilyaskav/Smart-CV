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
    public class InstitutionService : IInstitutionService
    {
        #region Declarations

        IInstitutionRepository _institutionRepository = null;

        #endregion

        public InstitutionService(IInstitutionRepository instRepo)
        {
            _institutionRepository = instRepo;
        }

        public void Dispose()
        {
            _institutionRepository.Dispose();
        }

        public void CreateInstitution(Models.InstitutionModel model)
        {
            _institutionRepository.Add(model.ToEntity());
        }

        public void UpdateInstitution(Models.InstitutionModel model)
        {
            if (model.Id == null) return;
            if (_institutionRepository.Has(model.Id.Value))
            {
                var entity = model.ToEntity();
                _institutionRepository.Update(entity);
            }
        }

        public void DeleteInstitution(int id)
        {
            if (_institutionRepository.Has(id)) _institutionRepository.Remove(id);
        }
    }
}
