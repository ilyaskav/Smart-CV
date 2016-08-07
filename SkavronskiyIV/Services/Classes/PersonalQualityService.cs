using Repository.Interfaces;
using Services.Interfaces;
using Services.Converters;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Classes
{
    public class PersonalQualityService:IPersonalQualityService
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
