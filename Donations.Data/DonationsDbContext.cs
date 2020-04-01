using Donations.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Donations.Data
{
    public class DonationsDbContext : IdentityDbContext<User, Role, Guid>
    {
        public virtual DbSet<Address> Address { get; set; }

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

            ConfigureAndSeedAddresses(builder);

            SeedRoles(builder);

            ConfigureAndSeedUsers(builder);
        }

        private void ConfigureAndSeedAddresses(ModelBuilder builder)
        {
            builder.Entity<Address>()
                .HasKey(p => p.Id);

            builder.Entity<Address>()
                .Property(p => p.Street1)
                .IsRequired();

            builder.Entity<Address>()
                .Property(p => p.City)
                .IsRequired();

            builder.Entity<Address>()
                .Property(p => p.Country)
                .IsRequired();

            builder.Entity<Address>()
                .Property(p => p.ZipCode)
                .IsRequired()
                .HasMaxLength(10);

            SeedData(builder, Seeding.Addresses.AdminAddress);
            SeedData(builder, Seeding.Addresses.UserAddress);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            SeedData(builder, Seeding.Roles.Admin);
            SeedData(builder, Seeding.Roles.User);
        }

        private void ConfigureAndSeedUsers(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasOne(p => p.Address)
                .WithOne(p => p.User)
                .HasForeignKey<User>(p => p.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

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
