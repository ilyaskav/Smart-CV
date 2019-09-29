using SmartCV.Entity.Classes;
using SmartCV.Repository.Interfaces;
using SmartCV.Entity;


namespace SmartCV.Repository.Classes
{
    public class WorkPlaceRepository : BaseRepository<WorkPlace>, IWorkPlaceRepository
    {
        public WorkPlaceRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
