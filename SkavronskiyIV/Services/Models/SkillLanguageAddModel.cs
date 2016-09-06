using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class SkillLanguageAddModel
    {
        public int? ResumeManagerId { get; set; }

        [UIHint("Skills")]
        public ICollection<SkillModel> Skills { get; set; }

        [UIHint("Languages")]
        public ICollection<LanguageModel> Languages { get; set; }

        public SkillLanguageAddModel()
        {
            Skills = new List<SkillModel>();
            Languages = new List<LanguageModel>();
        }
    }
}
