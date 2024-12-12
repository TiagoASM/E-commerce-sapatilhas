using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoEcommerceSapatilhas.Migrations
{
    /// <inheritdoc />
    public partial class TabelaTamanhos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tamanho",
                table: "Sapatilhas");

            migrationBuilder.CreateTable(
                name: "Tamanhos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SapatilhaId = table.Column<int>(type: "int", nullable: false),
                    TamanhoValor = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tamanhos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tamanhos_Sapatilhas_SapatilhaId",
                        column: x => x.SapatilhaId,
                        principalTable: "Sapatilhas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tamanhos_SapatilhaId",
                table: "Tamanhos",
                column: "SapatilhaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tamanhos");

            migrationBuilder.AddColumn<int>(
                name: "Tamanho",
                table: "Sapatilhas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
