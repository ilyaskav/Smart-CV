using Entities.Classes;
using Repository.Interfaces;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Converters;

namespace Services.Classes
{
    public class ProfessionService : IProfessionService
    {
        #region declarations

        IProfessionRepository _professionRepository = null;
        #endregion

        public ProfessionService(IProfessionRepository profRepo)
        {
            _professionRepository = profRepo;
        }

        public ICollection<Models.ProfessionModel> GetAll()
        {
            var entities = _professionRepository.Get();
            ICollection<ProfessionModel> models = new List<ProfessionModel>();
            foreach (var entity in entities)
            {
                models.Add(entity.ToModel());
            }
            return models;
            //return _professionRepository.Get().Select(entity => entity.ToModel()).ToList();
        }

        public void Dispose()
        {
            _professionRepository.Dispose();
        }
    }
}
