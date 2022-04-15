using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Tereza.Infrastructure.Migrations
{
    public partial class SeedNeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Need",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    IsCovered = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Need", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Need",
                columns: new[] { "Id", "Count", "Description", "IsCovered", "Name" },
                values: new object[,]
                {
                    { new Guid("82d257a5-d72b-4f08-bcf2-76ebdc958b5f"), 1, "Need laptop for working needs.", false, "Laptop" },
                    { new Guid("a961067e-c777-42c2-8fee-71180d750bd7"), 3, "Please, I can't find this drug in retail", false, "Aspirin" },
                    { new Guid("b47fdb0a-76d4-4b89-bf20-9cecfa4f4f82"), 1, "I need food for my cat, please help!", false, "Royal Canin Sphyncx 2 kg" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Need");
        }
    }
}
