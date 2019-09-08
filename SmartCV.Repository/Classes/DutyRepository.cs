using SmartCV.Entity.Classes;
using SmartCV.Repository.Interfaces;
using SmartCV.Entity;

namespace SmartCV.Repository.Classes
{
    public class DutyRepository : BaseRepository<Duty>, IDutyRepository
    {
        public DutyRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
