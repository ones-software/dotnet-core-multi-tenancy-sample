using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnet_core_multi_tenancy_sample.Migrations
{
    public partial class V0001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Caches",
                columns: table => new
                {
                    Key = table.Column<string>(maxLength: 255, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caches", x => x.Key);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Caches");
        }
    }
}
