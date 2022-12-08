using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class boxMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BoxId",
                table: "Birds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Boxes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    terrain = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boxes", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Birds_BoxId",
                table: "Birds",
                column: "BoxId");

            migrationBuilder.AddForeignKey(
                name: "FK_Birds_Boxes_BoxId",
                table: "Birds",
                column: "BoxId",
                principalTable: "Boxes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Birds_Boxes_BoxId",
                table: "Birds");

            migrationBuilder.DropTable(
                name: "Boxes");

            migrationBuilder.DropIndex(
                name: "IX_Birds_BoxId",
                table: "Birds");

            migrationBuilder.DropColumn(
                name: "BoxId",
                table: "Birds");
        }
    }
}
