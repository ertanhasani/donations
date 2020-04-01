using Donations.Data.Models;
using System;
namespace Donations.Data.Seeding
{
    public static class Addresses
    {
        public static Address AdminAddress => new Address
        {
            Id = Guid.Parse("a6c9b773-84ff-417c-9412-8226dc9f4192"),
            Street1 = "Kungsgatan 10",
            Street2 = "Lgh 1132",
            City = "Stockholm",
            Country = "Sweden",
            ZipCode = "11143"
        };

        public static Address UserAddress => new Address
        {
            Id = Guid.Parse("5317b0d0-ec46-4562-95af-ff4f2b513ae8"),
            Street1 = "Drottningsgatan 42",
            Street2 = "Tr 2 Lgh 2004",
            City = "Stockholm",
            Country = "Sweden",
            ZipCode = "11151"
        };
    }
}
