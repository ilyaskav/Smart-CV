using SmartCV.Entity.Classes;
using SmartCV.Repository.Interfaces;
using SmartCV.Entity;

namespace SmartCV.Repository.Classes
{
    public class PersonalDataRepository : BaseRepository<PersonalData>, IPersonalDataRepository
    {
        public PersonalDataRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
