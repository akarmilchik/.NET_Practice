using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ATS.DAL.Migrations
{
    public partial class ChangeSomeEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractToTariffPlanBindings");

            migrationBuilder.DropColumn(
                name: "CostCalculator_Id",
                table: "TariffPlans");

            migrationBuilder.AddColumn<int>(
                name: "ProvidedPort_Id",
                table: "Terminals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TariffPlan_ID",
                table: "Contracts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProvidedPort_Id",
                table: "Terminals");

            migrationBuilder.DropColumn(
                name: "TariffPlan_ID",
                table: "Contracts");

            migrationBuilder.AddColumn<int>(
                name: "CostCalculator_Id",
                table: "TariffPlans",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ContractToTariffPlanBindings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BindingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Contract_Id = table.Column<int>(type: "INTEGER", nullable: false),
                    TariffPlan_Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractToTariffPlanBindings", x => x.Id);
                });
        }
    }
}
