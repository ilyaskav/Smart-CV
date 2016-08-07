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
        void AddSkill(SkillModel model);
        void UpdateSkill(SkillModel model);
        void RemoveSkill(int id);
    }
}
