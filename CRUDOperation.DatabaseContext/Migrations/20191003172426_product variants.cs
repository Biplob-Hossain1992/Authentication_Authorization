using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUDOperation.DatabaseContext.Migrations
{
    public partial class productvariants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Variant_VariantId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Variant",
                table: "Variant");

            migrationBuilder.RenameTable(
                name: "Variant",
                newName: "Variants");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Variants",
                table: "Variants",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Variants_SizeId",
                table: "Variants",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Variants_VariantId",
                table: "Products",
                column: "VariantId",
                principalTable: "Variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Variants_Sizes_SizeId",
                table: "Variants",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Variants_VariantId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Variants_Sizes_SizeId",
                table: "Variants");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Variants",
                table: "Variants");

            migrationBuilder.DropIndex(
                name: "IX_Variants_SizeId",
                table: "Variants");

            migrationBuilder.RenameTable(
                name: "Variants",
                newName: "Variant");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Variant",
                table: "Variant",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Variant_VariantId",
                table: "Products",
                column: "VariantId",
                principalTable: "Variant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
