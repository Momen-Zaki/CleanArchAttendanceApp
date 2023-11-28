using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchAttendanceApp.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Init : Migration
  {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.CreateTable(
              name: "Users",
              columns: table => new
              {
                  Id = table.Column<Guid>(type: "TEXT", nullable: false),
                  FullName = table.Column<string>(type: "TEXT", nullable: true),
                  UserName = table.Column<string>(type: "TEXT", nullable: true),
                  PasswrodHash = table.Column<string>(type: "TEXT", nullable: true),
                  Role = table.Column<string>(type: "TEXT", nullable: true)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Users", x => x.Id);
              });

          migrationBuilder.CreateTable(
              name: "AttenanceRecords",
              columns: table => new
              {
                  Id = table.Column<Guid>(type: "TEXT", nullable: false),
                  AttendanceDay = table.Column<DateTime>(type: "TEXT", nullable: false),
                  ClockedIn = table.Column<bool>(type: "INTEGER", nullable: false),
                  ClockedInAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                  ClockedOut = table.Column<bool>(type: "INTEGER", nullable: false),
                  ClockedOutAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                  UserId = table.Column<Guid>(type: "TEXT", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_AttenanceRecords", x => x.Id);
                  table.ForeignKey(
                      name: "FK_AttenanceRecords_Users_UserId",
                      column: x => x.UserId,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
              });

          migrationBuilder.InsertData(
              table: "Users",
              columns: new[] { "Id", "FullName", "PasswrodHash", "Role", "UserName" },
              values: new object[,]
              {
                  { new Guid("4ed0544a-5aa3-4dc8-ba7c-640e4bc99b64"), "User 3 Full Name", "user 3 password hashed", "employee", "UserName3" },
                  { new Guid("97000964-dcd4-474f-8347-35d670af8588"), "User 2 Full Name", "user 2 password hashed", "employee", "UserName2" },
                  { new Guid("b9e19b58-9b89-45da-a126-36e2974fb7ce"), "User 1 Full Name", "user 1 password hashed", "employee", "UserName1" }
              });

          migrationBuilder.CreateIndex(
              name: "IX_AttenanceRecords_UserId",
              table: "AttenanceRecords",
              column: "UserId");
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.DropTable(
              name: "AttenanceRecords");

          migrationBuilder.DropTable(
              name: "Users");
      }
  }
