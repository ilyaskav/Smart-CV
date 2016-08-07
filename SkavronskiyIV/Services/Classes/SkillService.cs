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
    public class SkillService: ISkillService
    {
        #region Declarations

        ISkillRepository _skillRepository = null;

        #endregion

        public SkillService(ISkillRepository skillRepo)
        {
            _skillRepository = skillRepo;
        }


        public void AddSkill(Models.SkillModel model)
        {
            _skillRepository.Add(model.ToEntity());
        }

        public void UpdateSkill(Models.SkillModel model)
        {
            if (model.Id == null) return;
            if (_skillRepository.Has(model.Id.Value))
            {
                var entity = model.ToEntity();
                _skillRepository.Update(entity);
            }
        }

        public void RemoveSkill(int id)
        {
            if (_skillRepository.Has(id)) _skillRepository.Remove(id);
        }

        public void Dispose()
        {
            _skillRepository.Dispose();
        }
    }
}
