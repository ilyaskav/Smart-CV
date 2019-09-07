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
        IResumeManagerRepository _managerRepository = null;
        #endregion

        public ProfessionService(IProfessionRepository profRepo, IResumeManagerRepository managerRepo)
        {
            _professionRepository = profRepo;
            _managerRepository = managerRepo;
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
            var profession =_managerRepository.Get(managerId).Profession;
            return profession.Rules;
        }

        public void Dispose()
        {
            _professionRepository.Dispose();
        }
    }
}
