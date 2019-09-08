using SmartCV.Entity.Classes;
using SmartCV.Repository.Interfaces;
using SmartCV.Entity;


namespace SmartCV.Repository.Classes
{
    public class ResumeRepository : BaseRepository<Resume>, IResumeRepository
    {
        public ResumeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
