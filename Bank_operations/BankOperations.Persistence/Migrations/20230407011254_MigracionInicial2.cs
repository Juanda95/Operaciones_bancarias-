using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankOperations.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Identificacion = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Persona__234er45676677", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Clienteid = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Estado = table.Column<bool>(type: "bit", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Persona__234er45676680", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_Personas_Id",
                        column: x => x.Id,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cuentas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero_cuenta = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Tipo_cuenta = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Saldo_inicial = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<bool>(type: "bit", unicode: false, maxLength: 50, nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cuentas__3214EC07546653E7", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cuenta_Cliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    Tipo_movimiento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    valor = table.Column<int>(type: "int", nullable: false),
                    saldo = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdCuenta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Movimien__3214EC07DD5034B8", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movimientos_Cliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Movimientos_Cuenta",
                        column: x => x.IdCuenta,
                        principalTable: "Cuentas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cuentas_IdCliente",
                table: "Cuentas",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_IdCliente",
                table: "Movimientos",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_IdCuenta",
                table: "Movimientos",
                column: "IdCuenta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movimientos");

            migrationBuilder.DropTable(
                name: "Cuentas");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Personas");
        }
    }
}
