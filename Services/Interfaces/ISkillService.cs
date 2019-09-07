using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ISkillService: IDisposable
    {
        void CreateSkill(SkillModel model);

        SkillLanguageAddModel Get(int managerId);

        void CreateOrUpdate(SkillLanguageAddModel addModel);

        bool UpdateSkill(SkillModel model);

        void RemoveSkill(int id);
    }
}
