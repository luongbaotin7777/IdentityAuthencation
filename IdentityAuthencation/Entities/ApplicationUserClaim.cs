﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAuthencation.Entities
{
    public class ApplicationUserClaim: IdentityUserClaim<Guid>
    {
        public virtual ApplicationUser User { get; set; }
    }
}
