using Entities.Classes;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Repository.Classes
{
    public class CertificateRepository : BaseRepository<Certificate>, ICertificateRepository
    {
        public CertificateRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
