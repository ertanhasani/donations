using Donations.Data.Enums;
using Donations.Data.Models;
using System;

namespace Donations.Data.Seeding
{
    public static class Roles
    {
        public static Role Admin => GenerateRole(Guid.Parse("64cae1fc-4ab3-4c32-a498-50d780b6f84d"), UserRole.Admin);
        public static Role User => GenerateRole(Guid.Parse("06d5f950-2fd9-4ada-a536-33d8ee92a876"), UserRole.User);

        private static Role GenerateRole(Guid id, UserRole userRole)
        {
            var role = Enum.GetName(typeof(UserRole), userRole);

            return new Role
            {
                Id = id,
                Name = role,
                NormalizedName = role.ToUpper()
            };
        }
    }
}
