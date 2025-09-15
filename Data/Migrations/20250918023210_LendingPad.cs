using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class LendingPad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Identifier);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_Product_Order_OrderIdentifier",
                        column: x => x.OrderIdentifier,
                        principalTable: "Order",
                        principalColumn: "Identifier");
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Identifier", "Customer", "ProductId" },
                values: new object[,]
                {
                    { new Guid("78121743-27ac-4f16-bb75-9a388f79255f"), "Neville", new Guid("5ed5521b-c789-4c69-9aa5-c60cb478ab0a") },
                    { new Guid("ea54efcb-dea9-4ace-8975-3d2680ced971"), "Neville", new Guid("6d7e650e-c8e2-452a-a98c-f57b5e480db6") }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Identifier", "Description", "Name", "OrderIdentifier" },
                values: new object[,]
                {
                    { new Guid("5ed5521b-c789-4c69-9aa5-c60cb478ab0a"), "Industrial Robotic Arm", "Fanuc i90", null },
                    { new Guid("6d7e650e-c8e2-452a-a98c-f57b5e480db6"), "Industrial Robotic Arm", "Fanuc i30", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_OrderIdentifier",
                table: "Product",
                column: "OrderIdentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Order");
        }
    }
}
