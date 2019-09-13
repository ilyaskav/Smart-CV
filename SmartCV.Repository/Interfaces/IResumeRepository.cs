using SmartCV.Entity.Classes;

namespace SmartCV.Repository.Interfaces
{
    public interface IResumeRepository: IRepository<Resume>
    {
        void Clone(int managerId);
        
    }
}
