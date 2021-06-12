using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PatientPortal.Api.Migrations
{
    public partial class SeedWeb_ZipcodeString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7abd5154-db33-463f-99cf-24ecf2e48b82"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b11b41ef-7c6f-4e30-942f-79fd002b4533"));

            migrationBuilder.AlterColumn<string>(
                name: "Zipcode",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsDeleted", "Password", "PatientId", "Username" },
                values: new object[] { new Guid("e90416bb-5889-4db7-8e94-273fb63ab8d9"), false, "10000.WqcaDoKgiNEloduj0hELNA==.P7r8Tq20te86gr8ZpwJRs5JmvMo2wiHxsTA95C3ZyqM=", null, "bob" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsDeleted", "Password", "PatientId", "Username" },
                values: new object[] { new Guid("03797ec9-d933-4049-add4-8ba0803d4371"), false, "10000.iLl3E45elWO0WEJp4EVc9Q==.3a/Bo6vx2NXweAodzPhLTSiFmJYInMRYXPYMA4eLYfE=", null, "bill" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsDeleted", "Password", "PatientId", "Username" },
                values: new object[] { new Guid("6387dac1-a9ea-4d09-bef5-b275b44db78a"), false, "10000.yIRaKCUp9xVV86/W54uJfQ==.xm3v9vfaAQbYl07xBc3p/EeVnZH1ZFGwRAK0hZ6UTxg=", null, "patientportal.web" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("03797ec9-d933-4049-add4-8ba0803d4371"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6387dac1-a9ea-4d09-bef5-b275b44db78a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e90416bb-5889-4db7-8e94-273fb63ab8d9"));

            migrationBuilder.AlterColumn<int>(
                name: "Zipcode",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsDeleted", "Password", "PatientId", "Username" },
                values: new object[] { new Guid("b11b41ef-7c6f-4e30-942f-79fd002b4533"), false, "10000.WqcaDoKgiNEloduj0hELNA==.P7r8Tq20te86gr8ZpwJRs5JmvMo2wiHxsTA95C3ZyqM=", null, "bob" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsDeleted", "Password", "PatientId", "Username" },
                values: new object[] { new Guid("7abd5154-db33-463f-99cf-24ecf2e48b82"), false, "10000.iLl3E45elWO0WEJp4EVc9Q==.3a/Bo6vx2NXweAodzPhLTSiFmJYInMRYXPYMA4eLYfE=", null, "bill" });
        }
    }
}
