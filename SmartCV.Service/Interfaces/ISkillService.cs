using SmartCV.Service.Models;
using System;

namespace SmartCV.Service.Interfaces
{
    public interface ISkillService: IDisposable
    {
        void CreateSkill(SkillModel model);

        SkillLanguageAddModel Get(int managerId);

        void CreateOrUpdate(SkillLanguageAddModel addModel);

        bool UpdateSkill(SkillModel model);

        void RemoveSkill(int id);

        void RemoveLanguage(int id);
    }
}
