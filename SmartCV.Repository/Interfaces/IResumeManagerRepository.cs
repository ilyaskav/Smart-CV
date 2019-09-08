using SmartCV.Entity.Classes;

namespace SmartCV.Repository.Interfaces
{
    public interface IResumeManagerRepository: IRepository<ResumeManager>
    {
        

        void Clone(int managerId);
        
    }
}
