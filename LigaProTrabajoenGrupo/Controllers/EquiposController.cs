using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LigaProTrabajoenGrupo.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace LigaProTrabajoenGrupo.Controllers
{
    public class EquiposController : Controller
    {
        private readonly DBSqlSERVERLigaProECU _context;

        public EquiposController(DBSqlSERVERLigaProECU context)
        {
            _context = context;
        }

        // GET: Equipos
        public async Task<IActionResult> List()
        {
            List<Equipo> equipos = new List<Equipo>();

            // Crear las instancias de los equipos
            Equipo ldu = new Equipo
            {
                EquipoId = 1,  // Id autogenerado por la base de datos
                Nombre = "Liga Deportiva Universitaria",
                PartidosJugados = 10,
                PartidosGanados = 7,
                PartidosEmpatados = 2,
                PartidosPerdidos = 1
            };

            Equipo bsc = new Equipo
            {
                EquipoId = 2,
                Nombre = "Barcelona SC",
                PartidosJugados = 10,
                PartidosGanados = 6,
                PartidosEmpatados = 2,
                PartidosPerdidos = 2
            };

            Equipo emelec = new Equipo
            {
                EquipoId = 3,
                Nombre = "Emelec",
                PartidosJugados = 10,
                PartidosGanados = 5,
                PartidosEmpatados = 3,
                PartidosPerdidos = 2
            };

            Equipo elNacional = new Equipo
            {
                EquipoId = 4,
                Nombre = "El Nacional",
                PartidosJugados = 10,
                PartidosGanados = 4,
                PartidosEmpatados = 3,
                PartidosPerdidos = 3
            };

            Equipo delfinSC = new Equipo
            {
                EquipoId = 5,
                Nombre = "Delfín SC",
                PartidosJugados = 10,
                PartidosGanados = 3,
                PartidosEmpatados = 4,
                PartidosPerdidos = 3
            };

            // Agregar todos los equipos a la lista
            equipos.Add(ldu);
            equipos.Add(bsc);
            equipos.Add(emelec);
            equipos.Add(elNacional);
            equipos.Add(delfinSC);

            // Ahora puedes acceder a la lista de equipos y ver el total de puntos de cada uno
            foreach (var equipo in equipos)
            {
                Console.WriteLine($"{equipo.Nombre} - Puntos: {equipo.Puntos}");
            }

            return View(equipos);
        }

        // GET: Equipos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipo
                .FirstOrDefaultAsync(m => m.EquipoId == id);
            if (equipo == null)
            {
                return NotFound();
            }

            return View(equipo);
        }

        // GET: Equipos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipo.FindAsync(id);
            if (equipo == null)
            {
                return NotFound();
            }
            return View(equipo);
        }

        // POST: Equipos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EquipoId,Nombre,Descripcion,Logo,Presupuesto,PartidosJugados,PartidosGanados,PartidosEmpatados,PartidosPerdidos")] Equipo equipo, IFormFile Logo)
        {
            if (id != equipo.EquipoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Si se ha subido un logo, lo guardamos
                    if (Logo != null)
                    {
                        // Guardar el logo en la carpeta "wwwroot/images"
                        var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", Logo.FileName);

                        // Guardar el archivo en la carpeta
                        using (var stream = new FileStream(logoPath, FileMode.Create))
                        {
                            await Logo.CopyToAsync(stream);
                        }

                        // Guardar el nombre del archivo (nombre del logo) en la base de datos
                        equipo.Logo = Logo.FileName;
                    }

                    // Actualizar el equipo en la base de datos
                    _context.Update(equipo);
                    await _context.SaveChangesAsync();  // Guardamos los cambios en la base de datos
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipoExists(equipo.EquipoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(List));  // Redirigimos a la lista de equipos
            }
            return View(equipo);  // Si hay un error, devolvemos la vista con los errores
        }

        // GET: Equipos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipo
                .FirstOrDefaultAsync(m => m.EquipoId == id);
            if (equipo == null)
            {
                return NotFound();
            }

            return View(equipo);
        }

        // POST: Equipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipo = await _context.Equipo.FindAsync(id);
            if (equipo != null)
            {
                _context.Equipo.Remove(equipo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));  // Redirigimos a la lista de equipos
        }

        private bool EquipoExists(int id)
        {
            return _context.Equipo.Any(e => e.EquipoId == id);  // Verificamos si el equipo existe en la base de datos
        }
    }
}
