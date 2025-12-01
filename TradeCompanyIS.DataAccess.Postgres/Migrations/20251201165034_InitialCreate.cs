using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TradeCompanyIS.DataAccess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CountriesTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountriesTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    HashPassword = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientsTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NameClient = table.Column<string>(type: "text", nullable: false),
                    NumberPhone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    IdCountry = table.Column<Guid>(type: "uuid", nullable: false),
                    AddressDelivery = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientsTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientsTable_CountriesTable_IdCountry",
                        column: x => x.IdCountry,
                        principalTable: "CountriesTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProvidersTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NumberPhone = table.Column<string>(type: "text", nullable: false),
                    IdCountry = table.Column<Guid>(type: "uuid", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvidersTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProvidersTable_CountriesTable_IdCountry",
                        column: x => x.IdCountry,
                        principalTable: "CountriesTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WareHousesTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    IdCountry = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WareHousesTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WareHousesTable_CountriesTable_IdCountry",
                        column: x => x.IdCountry,
                        principalTable: "CountriesTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemsTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    IdWareHouse = table.Column<Guid>(type: "uuid", nullable: false),
                    QuantityWareHouse = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsTable_WareHousesTable_IdWareHouse",
                        column: x => x.IdWareHouse,
                        principalTable: "WareHousesTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OredersTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdClients = table.Column<Guid>(type: "uuid", nullable: false),
                    IdItem = table.Column<Guid>(type: "uuid", nullable: false),
                    DateReg = table.Column<DateOnly>(type: "date", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OredersTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OredersTable_ClientsTable_IdClients",
                        column: x => x.IdClients,
                        principalTable: "ClientsTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OredersTable_ItemsTable_IdItem",
                        column: x => x.IdItem,
                        principalTable: "ItemsTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SuppliesTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdProvider = table.Column<Guid>(type: "uuid", nullable: false),
                    IdWarehouse = table.Column<Guid>(type: "uuid", nullable: false),
                    IdItem = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuppliesTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuppliesTable_ItemsTable_IdItem",
                        column: x => x.IdItem,
                        principalTable: "ItemsTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SuppliesTable_ProvidersTable_IdProvider",
                        column: x => x.IdProvider,
                        principalTable: "ProvidersTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SuppliesTable_WareHousesTable_IdWarehouse",
                        column: x => x.IdWarehouse,
                        principalTable: "WareHousesTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientsTable_Email",
                table: "ClientsTable",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientsTable_IdCountry",
                table: "ClientsTable",
                column: "IdCountry");

            migrationBuilder.CreateIndex(
                name: "IX_CountriesTable_Name",
                table: "CountriesTable",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemsTable_IdWareHouse",
                table: "ItemsTable",
                column: "IdWareHouse");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsTable_Name",
                table: "ItemsTable",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OredersTable_IdClients",
                table: "OredersTable",
                column: "IdClients");

            migrationBuilder.CreateIndex(
                name: "IX_OredersTable_IdItem",
                table: "OredersTable",
                column: "IdItem");

            migrationBuilder.CreateIndex(
                name: "IX_ProvidersTable_IdCountry",
                table: "ProvidersTable",
                column: "IdCountry");

            migrationBuilder.CreateIndex(
                name: "IX_SuppliesTable_IdItem",
                table: "SuppliesTable",
                column: "IdItem");

            migrationBuilder.CreateIndex(
                name: "IX_SuppliesTable_IdProvider",
                table: "SuppliesTable",
                column: "IdProvider");

            migrationBuilder.CreateIndex(
                name: "IX_SuppliesTable_IdWarehouse",
                table: "SuppliesTable",
                column: "IdWarehouse");

            migrationBuilder.CreateIndex(
                name: "IX_WareHousesTable_IdCountry",
                table: "WareHousesTable",
                column: "IdCountry");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OredersTable");

            migrationBuilder.DropTable(
                name: "SuppliesTable");

            migrationBuilder.DropTable(
                name: "UsersTable");

            migrationBuilder.DropTable(
                name: "ClientsTable");

            migrationBuilder.DropTable(
                name: "ItemsTable");

            migrationBuilder.DropTable(
                name: "ProvidersTable");

            migrationBuilder.DropTable(
                name: "WareHousesTable");

            migrationBuilder.DropTable(
                name: "CountriesTable");
        }
    }
}
