using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Personkartotek.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonMigration",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonType = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    MiddleName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonMigration", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "ZipMigration",
                columns: table => new
                {
                    ZipId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: false),
                    ZipCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZipMigration", x => x.ZipId);
                });

            migrationBuilder.CreateTable(
                name: "EmailMigration",
                columns: table => new
                {
                    EmailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailMigration", x => x.EmailId);
                    table.ForeignKey(
                        name: "FK_144",
                        column: x => x.PersonId,
                        principalTable: "PersonMigration",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AddressMigration",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(nullable: false),
                    ZipId = table.Column<int>(nullable: false),
                    StreetName = table.Column<string>(nullable: false),
                    HouseNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressMigration", x => x.AddressId)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_138",
                        column: x => x.PersonId,
                        principalTable: "PersonMigration",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_100",
                        column: x => x.ZipId,
                        principalTable: "ZipMigration",
                        principalColumn: "ZipId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "fkIdx_138",
                table: "AddressMigration",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "fkIdx_100",
                table: "AddressMigration",
                column: "ZipId");

            migrationBuilder.CreateIndex(
                name: "fkIdx_144",
                table: "EmailMigration",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressMigration");

            migrationBuilder.DropTable(
                name: "EmailMigration");

            migrationBuilder.DropTable(
                name: "ZipMigration");

            migrationBuilder.DropTable(
                name: "PersonMigration");
        }
    }
}
