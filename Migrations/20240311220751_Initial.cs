using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace jexercise.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Address_Street = table.Column<string>(type: "TEXT", nullable: true),
                    Address_City = table.Column<string>(type: "TEXT", nullable: true),
                    Address_Zipcode = table.Column<string>(type: "TEXT", nullable: true),
                    Address_Country = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobOffers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address_City", "Address_Country", "Address_Street", "Address_Zipcode", "Name" },
                values: new object[,]
                {
                    { 1, "Rotterdam", "Netherlands", "Nassaukade 5", "3071 JL", "Jex" },
                    { 2, "Rotterdam", "Netherlands", "Nieuwe Binnenweg 79a", "3014 GB", "Koekela" },
                    { 3, "Rotterdam", "Netherlands", "", "3014 GE", "Bar 3" }
                });

            migrationBuilder.InsertData(
                table: "JobOffers",
                columns: new[] { "Id", "CompanyId", "Description", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Turn Code into Coffee", "Software Engineer" },
                    { 2, 2, "Turn Cake into Coffee", "Cake Engineer" },
                    { 3, 1, "Feed the children of Rotterdam", "CEO" },
                    { 4, 3, "Lekker biertjes tappen", "Bier Tapper" },
                    { 5, 3, "Spelen in een sopje", "Afwasser" },
                    { 6, 2, "Uitkijken met die hobbels", "Fietscourier" },
                    { 7, 1, "Turn food for children into Tests", "Test Engineer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Name",
                table: "Companies",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobOffers_CompanyId",
                table: "JobOffers",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobOffers");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
