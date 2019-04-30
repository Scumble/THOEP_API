using Microsoft.EntityFrameworkCore.Migrations;

namespace THOEP.DAL.Migrations
{
    public partial class fifth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthInfos_Diseases_DiseaseId",
                table: "HealthInfos");

            migrationBuilder.DropIndex(
                name: "IX_HealthInfos_DiseaseId",
                table: "HealthInfos");

            migrationBuilder.AlterColumn<string>(
                name: "DiseaseId",
                table: "HealthInfos",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DiseaseId1",
                table: "HealthInfos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HealthInfos_DiseaseId1",
                table: "HealthInfos",
                column: "DiseaseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthInfos_Diseases_DiseaseId1",
                table: "HealthInfos",
                column: "DiseaseId1",
                principalTable: "Diseases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthInfos_Diseases_DiseaseId1",
                table: "HealthInfos");

            migrationBuilder.DropIndex(
                name: "IX_HealthInfos_DiseaseId1",
                table: "HealthInfos");

            migrationBuilder.DropColumn(
                name: "DiseaseId1",
                table: "HealthInfos");

            migrationBuilder.AlterColumn<int>(
                name: "DiseaseId",
                table: "HealthInfos",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HealthInfos_DiseaseId",
                table: "HealthInfos",
                column: "DiseaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthInfos_Diseases_DiseaseId",
                table: "HealthInfos",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
