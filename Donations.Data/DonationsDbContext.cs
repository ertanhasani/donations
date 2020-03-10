using Donations.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Donations.Data
{
    public class DonationsDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DonationsDbContext(DbContextOptions<DonationsDbContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            SeedRoles(builder);

            SeedUsers(builder);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            SeedData(builder, Seeding.Roles.Admin);
            SeedData(builder, Seeding.Roles.User);
        }

        private void SeedUsers(ModelBuilder builder)
        {
            SeedData(builder, Seeding.Users.Administrator);
            SeedData(builder, Seeding.Users.User);

            foreach (var userRole in Seeding.Users.UserRoles)
            {
                SeedData(builder, userRole);
            }
        }

        private void SeedData<TEntity>(ModelBuilder builder, TEntity entity)
            where TEntity : class
        {
            builder.Entity<TEntity>()
                .HasData(entity);
        }
    }
}
