using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAuthencation.Dtos
{
    public class CreateRoleRequestDtos
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
    }
}
