using System;
using System.Collections.Generic;
using System.Text;

namespace Donations.Common.DTOs
{
    public class Address : BaseDto
    {
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}
