using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrinitasHunde
{
    public class ApplicationUser : IdentityUser
    {
        public bool LikesPizza { get; set; }
    }
}
