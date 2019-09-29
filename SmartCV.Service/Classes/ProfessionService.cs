using SmartCV.Repository.Interfaces;
using SmartCV.Service.Converters;
using SmartCV.Service.Interfaces;
using SmartCV.Service.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartCV.Service.Classes
{
    public class ProfessionService : IProfessionService
    {
        #region declarations

        readonly IProfessionRepository _professionRepository = null;

        #endregion

        public ProfessionService(IProfessionRepository profRepo)
        {
            _professionRepository = profRepo;
        }

        public ICollection<ProfessionModel> GetAll()
        {
            var entities = _professionRepository.Get();
            ICollection<ProfessionModel> models = new List<ProfessionModel>();
            foreach (var entity in entities)
            {
                models.Add(entity.ToModel());
            }
            return models;
        }

        public string GetRule(int managerId)
        {
            return _professionRepository
                .Get(p => p.Resumes.Any(rm => rm.Id == managerId))
                .Select(p => p.Rules)
                .FirstOrDefault();
        }

        public void Dispose()
        {
            _professionRepository.Dispose();
        }
    }
}
