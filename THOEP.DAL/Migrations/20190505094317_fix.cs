using Microsoft.EntityFrameworkCore.Migrations;

namespace THOEP.DAL.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivityPoints",
                table: "HealthInfos");

            migrationBuilder.DropColumn(
                name: "isFall",
                table: "HealthInfos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "ActivityPoints",
                table: "HealthInfos",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<bool>(
                name: "isFall",
                table: "HealthInfos",
                nullable: false,
                defaultValue: false);
        }
    }
}
