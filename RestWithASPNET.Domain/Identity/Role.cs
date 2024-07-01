using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestWithASPNET.Domain.Identity
{
    public class Role : IdentityRole<int>
    {
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
