using Entities;
using Entities.Classes;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Classes
{
    public class ResumeManagerRepository: BaseRepository<ResumeManager>, IResumeManagerRepository
    {
        public ResumeManagerRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
