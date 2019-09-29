using SmartCV.Entity.Classes;
using SmartCV.Repository.Interfaces;
using SmartCV.Entity;

namespace SmartCV.Repository.Classes
{
    public class PersonalQualityRepository : BaseRepository<PersonalQuality>, IPersonalQualityRepository
    {
        public PersonalQualityRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
