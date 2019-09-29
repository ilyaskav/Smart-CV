using SmartCV.Entity.Classes;
using SmartCV.Repository.Interfaces;
using SmartCV.Entity;

namespace SmartCV.Repository.Classes
{
    public class ContactTitleRepository : BaseRepository<ContactTitle>, IContactTitleRepository
    {
        public ContactTitleRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
