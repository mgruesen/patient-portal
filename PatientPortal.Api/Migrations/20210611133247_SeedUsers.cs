using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PatientPortal.Api.Migrations
{
    public partial class SeedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsDeleted", "Password", "PatientId", "Username" },
                values: new object[] { new Guid("b11b41ef-7c6f-4e30-942f-79fd002b4533"), false, "10000.WqcaDoKgiNEloduj0hELNA==.P7r8Tq20te86gr8ZpwJRs5JmvMo2wiHxsTA95C3ZyqM=", null, "bob" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsDeleted", "Password", "PatientId", "Username" },
                values: new object[] { new Guid("7abd5154-db33-463f-99cf-24ecf2e48b82"), false, "10000.iLl3E45elWO0WEJp4EVc9Q==.3a/Bo6vx2NXweAodzPhLTSiFmJYInMRYXPYMA4eLYfE=", null, "bill" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7abd5154-db33-463f-99cf-24ecf2e48b82"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b11b41ef-7c6f-4e30-942f-79fd002b4533"));
        }
    }
}
