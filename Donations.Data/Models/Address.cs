using System;
using System.Collections.Generic;
using System.Text;

namespace Donations.Data.Models
{
    public class Address : BaseEntity
    {
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        public virtual User User { get; set; }
    }
}
