using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Company = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    Address = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    City = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    State = table.Column<string>(type: "TEXT", maxLength: 35, nullable: true),
                    Zip = table.Column<string>(type: "TEXT", maxLength: 18, nullable: true),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 35, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 80, nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", maxLength: 35, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    At = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    Address = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    City = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    State = table.Column<string>(type: "TEXT", maxLength: 35, nullable: true),
                    Zip = table.Column<string>(type: "TEXT", maxLength: 18, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pay = table.Column<double>(type: "decimal(18,2)", nullable: false),
                    VenueId = table.Column<int>(type: "INTEGER", nullable: false),
                    BandId = table.Column<int>(type: "INTEGER", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gigs_Bands_BandId",
                        column: x => x.BandId,
                        principalTable: "Bands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gigs_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AmountDue = table.Column<double>(type: "decimal(18,2)", nullable: false),
                    AmountPaid = table.Column<double>(type: "decimal(18,2)", nullable: false),
                    DatePaid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Method = table.Column<string>(type: "TEXT", maxLength: 80, nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    GigId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payables_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payables_Gigs_GigId",
                        column: x => x.GigId,
                        principalTable: "Gigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receivables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AmountDue = table.Column<double>(type: "decimal(18,2)", nullable: false),
                    AmountPaid = table.Column<double>(type: "decimal(18,2)", nullable: false),
                    DateReceived = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Method = table.Column<string>(type: "TEXT", maxLength: 80, nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    GigId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receivables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receivables_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receivables_Gigs_GigId",
                        column: x => x.GigId,
                        principalTable: "Gigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gigs_BandId",
                table: "Gigs",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_Gigs_VenueId",
                table: "Gigs",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_Payables_EntityId",
                table: "Payables",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Payables_GigId",
                table: "Payables",
                column: "GigId");

            migrationBuilder.CreateIndex(
                name: "IX_Receivables_EntityId",
                table: "Receivables",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Receivables_GigId",
                table: "Receivables",
                column: "GigId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payables");

            migrationBuilder.DropTable(
                name: "Receivables");

            migrationBuilder.DropTable(
                name: "Entities");

            migrationBuilder.DropTable(
                name: "Gigs");

            migrationBuilder.DropTable(
                name: "Bands");

            migrationBuilder.DropTable(
                name: "Venues");
        }
    }
}
