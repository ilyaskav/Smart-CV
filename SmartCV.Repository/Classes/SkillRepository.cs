using SmartCV.Entity.Classes;
using SmartCV.Repository.Interfaces;
using SmartCV.Entity;


namespace SmartCV.Repository.Classes
{
    public class SkillRepository : BaseRepository<Skill>, ISkillRepository
    {
        public SkillRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
