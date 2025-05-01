using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LigaProTrabajoenGrupo.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reporte",
                columns: table => new
                {
                    ReporteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reporte", x => x.ReporteId);
                });

            migrationBuilder.CreateTable(
                name: "Equipo",
                columns: table => new
                {
                    EquipoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Presupuesto = table.Column<double>(type: "float", nullable: false),
                    PartidosJugados = table.Column<int>(type: "int", nullable: false),
                    PartidosGanados = table.Column<int>(type: "int", nullable: false),
                    PartidosEmpatados = table.Column<int>(type: "int", nullable: false),
                    PartidosPerdidos = table.Column<int>(type: "int", nullable: false),
                    ReporteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipo", x => x.EquipoId);
                    table.ForeignKey(
                        name: "FK_Equipo_Reporte_ReporteId",
                        column: x => x.ReporteId,
                        principalTable: "Reporte",
                        principalColumn: "ReporteId");
                });

            migrationBuilder.CreateTable(
                name: "FormularioPartido",
                columns: table => new
                {
                    FormularioPartidoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEquipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartidosJugados = table.Column<int>(type: "int", nullable: false),
                    PartidosGanados = table.Column<int>(type: "int", nullable: false),
                    PartidosEmpatados = table.Column<int>(type: "int", nullable: false),
                    PartidosPerdidos = table.Column<int>(type: "int", nullable: false),
                    EquipoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormularioPartido", x => x.FormularioPartidoId);
                    table.ForeignKey(
                        name: "FK_FormularioPartido_Equipo_EquipoId",
                        column: x => x.EquipoId,
                        principalTable: "Equipo",
                        principalColumn: "EquipoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Jugador",
                columns: table => new
                {
                    JugadorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NumeroCamiseta = table.Column<int>(type: "int", nullable: false),
                    Goles = table.Column<int>(type: "int", nullable: false),
                    Asistencias = table.Column<int>(type: "int", nullable: false),
                    Sueldo = table.Column<double>(type: "float", nullable: false),
                    EquipoId = table.Column<int>(type: "int", nullable: false),
                    ReporteId = table.Column<int>(type: "int", nullable: true),
                    ReporteId1 = table.Column<int>(type: "int", nullable: true),
                    ReporteId2 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jugador", x => x.JugadorId);
                    table.ForeignKey(
                        name: "FK_Jugador_Equipo_EquipoId",
                        column: x => x.EquipoId,
                        principalTable: "Equipo",
                        principalColumn: "EquipoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Jugador_Reporte_ReporteId",
                        column: x => x.ReporteId,
                        principalTable: "Reporte",
                        principalColumn: "ReporteId");
                    table.ForeignKey(
                        name: "FK_Jugador_Reporte_ReporteId1",
                        column: x => x.ReporteId1,
                        principalTable: "Reporte",
                        principalColumn: "ReporteId");
                    table.ForeignKey(
                        name: "FK_Jugador_Reporte_ReporteId2",
                        column: x => x.ReporteId2,
                        principalTable: "Reporte",
                        principalColumn: "ReporteId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipo_ReporteId",
                table: "Equipo",
                column: "ReporteId");

            migrationBuilder.CreateIndex(
                name: "IX_FormularioPartido_EquipoId",
                table: "FormularioPartido",
                column: "EquipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Jugador_EquipoId",
                table: "Jugador",
                column: "EquipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Jugador_ReporteId",
                table: "Jugador",
                column: "ReporteId");

            migrationBuilder.CreateIndex(
                name: "IX_Jugador_ReporteId1",
                table: "Jugador",
                column: "ReporteId1");

            migrationBuilder.CreateIndex(
                name: "IX_Jugador_ReporteId2",
                table: "Jugador",
                column: "ReporteId2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormularioPartido");

            migrationBuilder.DropTable(
                name: "Jugador");

            migrationBuilder.DropTable(
                name: "Equipo");

            migrationBuilder.DropTable(
                name: "Reporte");
        }
    }
}
