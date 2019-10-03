using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUDOperation.DatabaseContext.Migrations
{
    public partial class Setnullablefortheforeignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Variants_VariantId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Variants_Sizes_SizeId",
                table: "Variants");

            migrationBuilder.AlterColumn<long>(
                name: "SizeId",
                table: "Variants",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "VariantId",
                table: "Products",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Variants_VariantId",
                table: "Products",
                column: "VariantId",
                principalTable: "Variants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Variants_Sizes_SizeId",
                table: "Variants",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Variants_VariantId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Variants_Sizes_SizeId",
                table: "Variants");

            migrationBuilder.AlterColumn<long>(
                name: "SizeId",
                table: "Variants",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "VariantId",
                table: "Products",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

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
    }
}
