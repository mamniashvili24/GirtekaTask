using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class Init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "HouseholdMeteringPlants",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PLT = table.Column<DateTime>(name: "PL_T", type: "datetime2", nullable: false),
                PPlus = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                TINKLAS = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                PMinus = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                OBJNUMERIS = table.Column<int>(name: "OBJ_NUMERIS", type: "int", nullable: false),
                OBJGVTIPAS = table.Column<string>(name: "OBJ_GV_TIPAS", type: "nvarchar(16)", maxLength: 16, nullable: true),
                OBTPAVADINIMAS = table.Column<string>(name: "OBT_PAVADINIMAS", type: "nvarchar(32)", maxLength: 32, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_HouseholdMeteringPlants", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "HouseholdMeteringPlants");
    }
}