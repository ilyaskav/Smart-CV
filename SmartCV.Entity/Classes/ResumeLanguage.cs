using System.ComponentModel.DataAnnotations;

namespace SmartCV.Entity.Classes
{
    public class ResumeLanguage : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int ResumeId { get; set; }
        public int LanguageId { get; set; }


        #region navigation

        public virtual Resume Resume { get; set; }
        public virtual Language Language { get; set; }

        #endregion

    }
}
