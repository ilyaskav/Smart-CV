using SmartCV.Repository.Interfaces;
using SmartCV.Service.Converters;
using SmartCV.Service.Interfaces;
using SmartCV.Service.Models;
using System;
using System.Collections.Generic;

namespace SmartCV.Service.Classes
{
    public class LanguageService:ILanguageService
    {
        #region Declarations

        ILanguageRepository _languageRepository = null;

        #endregion

        public LanguageService(ILanguageRepository langRepo)
        {
            _languageRepository = langRepo;
        }



        public void AddLanguage(Models.LanguageModel model)
        {
            // Узнать как лучше проверять на подобность
            _languageRepository.Add(model.ToEntity());
        }

        public void UpdateLanguage(Models.LanguageModel model)
        {
            if (model.Id == null) return;
            if (_languageRepository.Has(model.Id.Value))
            {
                var entity = model.ToEntity();
                _languageRepository.Update(entity);
            }
        }

        public void DeleteLanguage(int id)
        {
            // доделать
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _languageRepository.Dispose();
        }
    }
}
