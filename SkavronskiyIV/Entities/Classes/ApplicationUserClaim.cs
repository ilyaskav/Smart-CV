﻿using Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Classes
{
    public class ApplicationUserClaim : IdentityUserClaim<int>, IEntity
    {
    }
}
