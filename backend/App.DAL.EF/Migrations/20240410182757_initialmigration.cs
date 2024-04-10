using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AdditionalInformation = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MethodName = table.Column<string>(type: "text", nullable: false),
                    MethodDescription = table.Column<string>(type: "text", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    RegistryCode = table.Column<string>(type: "text", nullable: false),
                    ParticipantCount = table.Column<int>(type: "integer", nullable: false),
                    AdditionalNotes = table.Column<string>(type: "text", nullable: false),
                    PaymentMethodId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_ExerciseResults_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "ExerciseResults",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    PersonalIdentificationNumber = table.Column<int>(type: "integer", nullable: false),
                    AdditionalNotes = table.Column<string>(type: "text", nullable: false),
                    PaymentMethodId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_ExerciseResults_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "ExerciseResults",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExerciseMadeFors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RegisterDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: true),
                    FirmId = table.Column<Guid>(type: "uuid", nullable: true),
                    EventId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseMadeFors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseMadeFors_Accounts_EventId",
                        column: x => x.EventId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExerciseMadeFors_Exercises_FirmId",
                        column: x => x.FirmId,
                        principalTable: "Exercises",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExerciseMadeFors_Games_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Games",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseMadeFors_EventId",
                table: "ExerciseMadeFors",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseMadeFors_FirmId",
                table: "ExerciseMadeFors",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseMadeFors_PersonId",
                table: "ExerciseMadeFors",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_PaymentMethodId",
                table: "Exercises",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_PaymentMethodId",
                table: "Games",
                column: "PaymentMethodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseMadeFors");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "ExerciseResults");
        }
    }
}
