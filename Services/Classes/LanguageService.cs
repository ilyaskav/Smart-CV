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
