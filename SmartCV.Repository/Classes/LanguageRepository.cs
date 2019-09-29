using SmartCV.Entity.Classes;
using SmartCV.Repository.Interfaces;
using SmartCV.Entity;

namespace SmartCV.Repository.Classes
{
    public class LanguageRepository : BaseRepository<Language>, ILanguageRepository
    {
        public LanguageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
