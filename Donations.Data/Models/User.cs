using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Donations.Data.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FullName { get; set; }
    }
}
