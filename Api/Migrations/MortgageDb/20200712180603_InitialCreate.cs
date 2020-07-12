using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations.MortgageDb
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mortgages",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Lender = table.Column<string>(nullable: true),
                    Rate = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    LoanToValue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mortgages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mortgages");
        }
    }
}
