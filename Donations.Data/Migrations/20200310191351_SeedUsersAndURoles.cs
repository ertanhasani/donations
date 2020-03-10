using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Donations.Data.Migrations
{
    public partial class SeedUsersAndURoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("64cae1fc-4ab3-4c32-a498-50d780b6f84d"), "25a98498-dc28-484b-8474-c65ae0933989", "Admin", "ADMIN" },
                    { new Guid("06d5f950-2fd9-4ada-a536-33d8ee92a876"), "3215d22c-56b1-4178-aa87-0db5f8ed4e97", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("5a6333f1-9696-4b1c-a8f8-04619ebd686d"), 0, "5c3b44c6-61ac-499a-a3eb-466fc352ea2a", "ertanhasani96@gmail.com", true, "Admin Admin", false, null, "ERTANHASANI96@GMAIL.COM", "ERTANHASANI96@GMAIL.COM", "AQAAAAEAACcQAAAAEGGq/3SM3A1BmBil3fkkEPqB98hCtMBIZfbY/AdviFfMNQ616UwAIMDwUZWAW7nbGQ==", null, true, "70103f02-da69-4171-9aaa-fb42e720fa48", false, "ertanhasani96@gmail.com" },
                    { new Guid("a66a45b8-20eb-4e4c-975a-d49be470e0df"), 0, "14dd3898-04ce-40e1-afc6-cd7c8bce7d81", "test@gmail.com", true, "User user", false, null, "TEST@GMAIL.COM", "TEST@GMAIL.COM", "AQAAAAEAACcQAAAAEC2eJDhajSUmwdgROf9s1lka6ohkm5nNLtIfManeJIzFr/GuvghPtPPv13mB+mwIyA==", null, true, "0ef0f4da-09d0-4a83-9de6-9d811fac178e", false, "test@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("5a6333f1-9696-4b1c-a8f8-04619ebd686d"), new Guid("64cae1fc-4ab3-4c32-a498-50d780b6f84d") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("a66a45b8-20eb-4e4c-975a-d49be470e0df"), new Guid("06d5f950-2fd9-4ada-a536-33d8ee92a876") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("5a6333f1-9696-4b1c-a8f8-04619ebd686d"), new Guid("64cae1fc-4ab3-4c32-a498-50d780b6f84d") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("a66a45b8-20eb-4e4c-975a-d49be470e0df"), new Guid("06d5f950-2fd9-4ada-a536-33d8ee92a876") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("06d5f950-2fd9-4ada-a536-33d8ee92a876"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("64cae1fc-4ab3-4c32-a498-50d780b6f84d"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5a6333f1-9696-4b1c-a8f8-04619ebd686d"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a66a45b8-20eb-4e4c-975a-d49be470e0df"));
        }
    }
}
