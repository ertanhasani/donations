using System;
using System.Collections.Generic;
using System.Text;

namespace Donations.Data.Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
