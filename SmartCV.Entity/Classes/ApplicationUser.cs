using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SmartCV.Entity.Classes
{
    public class ApplicationUser : IdentityUser<long>
    {
        public virtual ICollection<Resume> Resumes { get; set; }

        public ApplicationUser()
        {
            Resumes = new List<Resume>();
        }

    }
}
