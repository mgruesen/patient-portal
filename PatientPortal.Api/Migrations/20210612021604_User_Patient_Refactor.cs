using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PatientPortal.Api.Migrations
{
    public partial class User_Patient_Refactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Contacts_ContactId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Providers_ProviderId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_ContactId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_ProviderId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "Patients");

            migrationBuilder.AddColumn<Guid>(
                name: "ContactId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProviderId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ContactId",
                table: "Users",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProviderId",
                table: "Users",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Contacts_ContactId",
                table: "Users",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Providers_ProviderId",
                table: "Users",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Contacts_ContactId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Providers_ProviderId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ContactId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProviderId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "ContactId",
                table: "Patients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProviderId",
                table: "Patients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_ContactId",
                table: "Patients",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_ProviderId",
                table: "Patients",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Contacts_ContactId",
                table: "Patients",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Providers_ProviderId",
                table: "Patients",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
