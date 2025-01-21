using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InovaFinancas.Api.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: true),
                    Slug = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: true),
                    IsAtivo = table.Column<bool>(type: "BIT", nullable: false),
                    Valor = table.Column<decimal>(type: "MONEY", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Voucher",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(type: "CHAR(8)", maxLength: 8, nullable: false),
                    Titulo = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: true),
                    IsAtivo = table.Column<bool>(type: "BIT", nullable: false),
                    Valor = table.Column<decimal>(type: "MONEY", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdutoId = table.Column<long>(type: "bigint", nullable: false),
                    Numero = table.Column<string>(type: "CHAR(8)", maxLength: 8, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    ReferenciaExterna = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: true),
                    Gateway = table.Column<short>(type: "SMALLINT", nullable: false),
                    Estado = table.Column<short>(type: "SMALLINT", nullable: false),
                    VoucherId = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<string>(type: "VARCHAR(160)", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedido_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pedido_Voucher_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Voucher",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_ProdutoId",
                table: "Pedido",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_VoucherId",
                table: "Pedido",
                column: "VoucherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Voucher");
        }
    }
}
