using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LigaProTrabajoenGrupo.Models;

    public class DBSqlSERVERLigaProECU : DbContext
    {
        public DBSqlSERVERLigaProECU (DbContextOptions<DBSqlSERVERLigaProECU> options)
            : base(options)
        {
        }

        public DbSet<LigaProTrabajoenGrupo.Models.Equipo> Equipo { get; set; } = default!;

public DbSet<LigaProTrabajoenGrupo.Models.Jugador> Jugador { get; set; } = default!;

public DbSet<LigaProTrabajoenGrupo.Models.FormularioPartido> FormularioPartido { get; set; } = default!;

public DbSet<LigaProTrabajoenGrupo.Models.Reporte> Reporte { get; set; } = default!;
    }
