using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LigaProTrabajoenGrupo.Models;
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
            // Traemos todos los equipos desde la base de datos
            var equipos = await _context.Equipo.ToListAsync();

            // Si no tienes equipos en la base de datos, los predefinidos aparecerán aquí
            if (equipos.Count == 0)
            {
                // Crear las instancias de los equipos predefinidos solo si no hay equipos en la base de datos
                Equipo ldu = new Equipo
                {
                    EquipoId = 1,
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

                
                equipos.Add(ldu);
                equipos.Add(bsc);
                equipos.Add(emelec);
                equipos.Add(elNacional);
                equipos.Add(delfinSC);
            }

            return View(equipos); // Retornar la vista con los equipos, ya sean predefinidos o guardados
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

        // GET: Equipos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Equipos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EquipoId,Nombre,Descripcion,Logo,Presupuesto,PartidosJugados,PartidosGanados,PartidosEmpatados,PartidosPerdidos")] Equipo equipo, IFormFile Logo)
        {
            if (ModelState.IsValid)
            {
                if (Logo != null)
                {
                    var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", Logo.FileName);
                    using (var stream = new FileStream(logoPath, FileMode.Create))
                    {
                        await Logo.CopyToAsync(stream);
                    }
                    equipo.Logo = Logo.FileName;
                }

                _context.Add(equipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
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
                    if (Logo != null)
                    {
                        var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", Logo.FileName);
                        using (var stream = new FileStream(logoPath, FileMode.Create))
                        {
                            await Logo.CopyToAsync(stream);
                        }
                        equipo.Logo = Logo.FileName;
                    }

                    _context.Update(equipo);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(List));
            }
            return View(equipo);
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
            return RedirectToAction(nameof(List));
        }

        private bool EquipoExists(int id)
        {
            return _context.Equipo.Any(e => e.EquipoId == id);
        }
    }
}
