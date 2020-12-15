using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAuthencation.Dtos
{
    public class CreateRoleRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
