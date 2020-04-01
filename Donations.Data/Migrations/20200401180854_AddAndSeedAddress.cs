using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Donations.Data.Migrations
{
    public partial class AddAndSeedAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Street1 = table.Column<string>(nullable: false),
                    Street2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: false),
                    ZipCode = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "City", "Country", "IsDeleted", "Street1", "Street2", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("a6c9b773-84ff-417c-9412-8226dc9f4192"), "Stockholm", "Sweden", false, "Kungsgatan 10", "Lgh 1132", "11143" },
                    { new Guid("5317b0d0-ec46-4562-95af-ff4f2b513ae8"), "Stockholm", "Sweden", false, "Drottningsgatan 42", "Tr 2 Lgh 2004", "11151" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("06d5f950-2fd9-4ada-a536-33d8ee92a876"),
                column: "ConcurrencyStamp",
                value: "4849cb35-cba8-4230-9530-a74a33f2fd4a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("64cae1fc-4ab3-4c32-a498-50d780b6f84d"),
                column: "ConcurrencyStamp",
                value: "c82f7f1b-7fe8-41c5-adc1-460cc30937da");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5a6333f1-9696-4b1c-a8f8-04619ebd686d"),
                columns: new[] { "AddressId", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { new Guid("a6c9b773-84ff-417c-9412-8226dc9f4192"), "73334b17-f14c-458c-8831-f5f75e5add9e", "AQAAAAEAACcQAAAAEDXMxy7bQBmV7OQZyFA65KNb6DocgMHO3SvmAdo6f7Tdrpxi8dSMJLIuSWtGg9MXvw==", "c52fb4d7-f7f2-45b1-81dd-3dc6c73e6a04" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a66a45b8-20eb-4e4c-975a-d49be470e0df"),
                columns: new[] { "AddressId", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { new Guid("5317b0d0-ec46-4562-95af-ff4f2b513ae8"), "198c027e-ae6e-4e9d-b775-d2457985ba9f", "AQAAAAEAACcQAAAAEMoXWlsuPCrCEuvJ8eO4z7c/6tpPFjj6P0vaS3aGQP9YL/+K1UDB2IuRVeD2mO3zYg==", "a0f92cec-e63c-4938-bb3d-1e1cf6498f23" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AddressId",
                table: "AspNetUsers",
                column: "AddressId",
                unique: true,
                filter: "[AddressId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Address_AddressId",
                table: "AspNetUsers",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Address_AddressId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AddressId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("06d5f950-2fd9-4ada-a536-33d8ee92a876"),
                column: "ConcurrencyStamp",
                value: "3215d22c-56b1-4178-aa87-0db5f8ed4e97");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("64cae1fc-4ab3-4c32-a498-50d780b6f84d"),
                column: "ConcurrencyStamp",
                value: "25a98498-dc28-484b-8474-c65ae0933989");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5a6333f1-9696-4b1c-a8f8-04619ebd686d"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5c3b44c6-61ac-499a-a3eb-466fc352ea2a", "AQAAAAEAACcQAAAAEGGq/3SM3A1BmBil3fkkEPqB98hCtMBIZfbY/AdviFfMNQ616UwAIMDwUZWAW7nbGQ==", "70103f02-da69-4171-9aaa-fb42e720fa48" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a66a45b8-20eb-4e4c-975a-d49be470e0df"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "14dd3898-04ce-40e1-afc6-cd7c8bce7d81", "AQAAAAEAACcQAAAAEC2eJDhajSUmwdgROf9s1lka6ohkm5nNLtIfManeJIzFr/GuvghPtPPv13mB+mwIyA==", "0ef0f4da-09d0-4a83-9de6-9d811fac178e" });
        }
    }
}
