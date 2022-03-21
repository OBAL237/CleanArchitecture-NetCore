using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Data.Migrations
{
    public partial class _20211217_1659 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Commandes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    NumeroCommande = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    Reference = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    DateEnregistrement = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commandes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Libelle = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Reference = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    DateEnregistrement = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeProprietes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Libelle = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    EstArchive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeProprietes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LigneCommandes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    ProduitId = table.Column<Guid>(type: "char(36)", nullable: false),
                    CommandeId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Quantite = table.Column<int>(type: "int", nullable: false),
                    Prix = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LigneCommandes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LigneCommandes_Commandes_CommandeId",
                        column: x => x.CommandeId,
                        principalTable: "Commandes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LigneCommandes_Produits_ProduitId",
                        column: x => x.ProduitId,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proprietes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    ProduitId = table.Column<Guid>(type: "char(36)", nullable: false),
                    TypeProprieteId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Valeur = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proprietes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proprietes_Produits_ProduitId",
                        column: x => x.ProduitId,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proprietes_TypeProprietes_TypeProprieteId",
                        column: x => x.TypeProprieteId,
                        principalTable: "TypeProprietes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LigneCommandes_CommandeId",
                table: "LigneCommandes",
                column: "CommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_LigneCommandes_ProduitId",
                table: "LigneCommandes",
                column: "ProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_Proprietes_ProduitId",
                table: "Proprietes",
                column: "ProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_Proprietes_TypeProprieteId",
                table: "Proprietes",
                column: "TypeProprieteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LigneCommandes");

            migrationBuilder.DropTable(
                name: "Proprietes");

            migrationBuilder.DropTable(
                name: "Commandes");

            migrationBuilder.DropTable(
                name: "Produits");

            migrationBuilder.DropTable(
                name: "TypeProprietes");
        }
    }
}
