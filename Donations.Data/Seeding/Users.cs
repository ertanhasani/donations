using Donations.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;

namespace Donations.Data.Seeding
{
    public static class Users
    {
        public static User Administrator => GenerateUser(Guid.Parse("5a6333f1-9696-4b1c-a8f8-04619ebd686d"), "Admin Admin", "ertanhasani96@gmail.com", Addresses.AdminAddress.Id);
        public static User User => GenerateUser(Guid.Parse("a66a45b8-20eb-4e4c-975a-d49be470e0df"), "User user", "test@gmail.com", Addresses.UserAddress.Id);

        private static PasswordHasher<User> _hasher = new PasswordHasher<User>();
        private static User GenerateUser(Guid id, string fullName, string email, Guid addressId)
        {
            var user = new User
            {
                Id = id,
                FullName = fullName,
                Email = email,
                NormalizedEmail = email.ToUpper(),
                EmailConfirmed = true,
                UserName = email,
                NormalizedUserName = email.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(),
                PhoneNumberConfirmed = true,
                LockoutEnabled = false,
                TwoFactorEnabled = false,
                AccessFailedCount = 0,
                AddressId = addressId
            };
            user.PasswordHash = _hasher.HashPassword(user, "password");

            return user;
        }

        public static IdentityUserRole<Guid>[] UserRoles => new IdentityUserRole<Guid>[]
        {
            new IdentityUserRole<Guid> { UserId = Administrator.Id, RoleId = Roles.Admin.Id },
            new IdentityUserRole<Guid> { UserId = User.Id, RoleId = Roles.User.Id }
        };
    }

}
