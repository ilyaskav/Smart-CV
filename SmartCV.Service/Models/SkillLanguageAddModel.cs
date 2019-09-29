using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartCV.Service.Models
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
