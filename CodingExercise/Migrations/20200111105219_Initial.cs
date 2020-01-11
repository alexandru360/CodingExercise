using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodingExercise.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessPayment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreditCardNumber = table.Column<long>(nullable: false),
                    CardHolder = table.Column<string>(maxLength: 250, nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    SecurityCode = table.Column<string>(maxLength: 19, nullable: false),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessPayment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentState",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PmntState = table.Column<string>(nullable: false),
                    ProcessPaymentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentState", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentState_ProcessPayment_ProcessPaymentId",
                        column: x => x.ProcessPaymentId,
                        principalTable: "ProcessPayment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentState_ProcessPaymentId",
                table: "PaymentState",
                column: "ProcessPaymentId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentState");

            migrationBuilder.DropTable(
                name: "ProcessPayment");
        }
    }
}
