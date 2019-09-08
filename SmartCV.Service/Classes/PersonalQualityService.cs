using SmartCV.Repository.Interfaces;
using SmartCV.Service.Converters;
using SmartCV.Service.Interfaces;
using SmartCV.Service.Models;
using System.Collections.Generic;

namespace SmartCV.Service.Classes
{
    public class PersonalQualityService : IPersonalQualityService
    {
        #region Declarations

        IPersonalQualityRepository _personalQualityRepository = null;

        #endregion

        public PersonalQualityService(IPersonalQualityRepository qualityRepo)
        {
            _personalQualityRepository = qualityRepo;
        }


        public void AddPersonalQuality(PersonalQualityModel model)
        {
            _personalQualityRepository.Add(model.ToEntity());
        }

        public void UpdatePersonalQuality(PersonalQualityModel model)
        {
            if (model.Id == null) return;
            if (_personalQualityRepository.Has(model.Id.Value))
            {
                var entity = model.ToEntity();
                _personalQualityRepository.Update(entity);
            }
        }

        public void RemovePersonalQuality(int id)
        {
            if (_personalQualityRepository.Has(id)) _personalQualityRepository.Remove(id);
        }

        public void Dispose()
        {
            _personalQualityRepository.Dispose();
        }
    }
}
