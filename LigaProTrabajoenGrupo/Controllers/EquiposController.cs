using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using LigaProTrabajoenGrupo.Models;

namespace LigaProTrabajoenGrupo.Controllers
{
    public class EquiposController : Controller
    {
        // Lista en memoria para mantener los equipos
        private static List<Equipo> equipos = new List<Equipo>
        {
            new Equipo
            {
                EquipoId = 1,
                Nombre = "Liga Deportiva Universitaria",
                PartidosJugados = 10,
                PartidosGanados = 7,
                PartidosEmpatados = 2,
                PartidosPerdidos = 1
            },
            new Equipo
            {
                EquipoId = 2,
                Nombre = "Barcelona SC",
                PartidosJugados = 10,
                PartidosGanados = 6,
                PartidosEmpatados = 2,
                PartidosPerdidos = 2
            },
            new Equipo
            {
                EquipoId = 3,
                Nombre = "Emelec",
                PartidosJugados = 10,
                PartidosGanados = 5,
                PartidosEmpatados = 3,
                PartidosPerdidos = 2
            },
            new Equipo
            {
                EquipoId = 4,
                Nombre = "El Nacional",
                PartidosJugados = 10,
                PartidosGanados = 4,
                PartidosEmpatados = 3,
                PartidosPerdidos = 3
            },
            new Equipo
            {
                EquipoId = 5,
                Nombre = "Delfín SC",
                PartidosJugados = 10,
                PartidosGanados = 3,
                PartidosEmpatados = 4,
                PartidosPerdidos = 3
            }
        };

        // GET: Equipos
        public IActionResult List()
        {
            // Calcular los puntos para cada equipo
            foreach (var equipo in equipos)
            {
                equipo.Puntos = (equipo.PartidosGanados * 3) + (equipo.PartidosEmpatados);
            }

            return View(equipos); // Retornamos la vista con la lista de equipos
        }

        // GET: Equipos/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = equipos.FirstOrDefault(e => e.EquipoId == id);
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
        public IActionResult Create([Bind("EquipoId,Nombre,Descripcion,Logo,Presupuesto,PartidosJugados,PartidosGanados,PartidosEmpatados,PartidosPerdidos")] Equipo equipo, IFormFile Logo)
        {
            if (ModelState.IsValid)
            {
                // Si se ha subido un logo, lo guardamos
                if (Logo != null)
                {
                    var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", Logo.FileName);
                    using (var stream = new FileStream(logoPath, FileMode.Create))
                    {
                        Logo.CopyTo(stream);
                    }
                    equipo.Logo = Logo.FileName;
                }

                // Asignar un nuevo ID de equipo
                equipo.EquipoId = equipos.Count + 1;

                // Agregar el equipo a la lista en memoria
                equipos.Add(equipo);

                return RedirectToAction(nameof(List));
            }
            return View(equipo);
        }

        // GET: Equipos/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = equipos.FirstOrDefault(e => e.EquipoId == id);
            if (equipo == null)
            {
                return NotFound();
            }

            return View(equipo);
        }

        // POST: Equipos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("EquipoId,Nombre,Descripcion,Logo,Presupuesto,PartidosJugados,PartidosGanados,PartidosEmpatados,PartidosPerdidos")] Equipo equipo, IFormFile Logo)
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
                        var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", Logo.FileName);
                        using (var stream = new FileStream(logoPath, FileMode.Create))
                        {
                            Logo.CopyTo(stream);
                        }
                        equipo.Logo = Logo.FileName;
                    }

                    // Actualizamos el equipo en la lista
                    var equipoExistente = equipos.FirstOrDefault(e => e.EquipoId == id);
                    if (equipoExistente != null)
                    {
                        equipoExistente.Nombre = equipo.Nombre;
                        equipoExistente.Descripcion = equipo.Descripcion;
                        equipoExistente.Logo = equipo.Logo;
                        equipoExistente.Presupuesto = equipo.Presupuesto;
                        equipoExistente.PartidosJugados = equipo.PartidosJugados;
                        equipoExistente.PartidosGanados = equipo.PartidosGanados;
                        equipoExistente.PartidosEmpatados = equipo.PartidosEmpatados;
                        equipoExistente.PartidosPerdidos = equipo.PartidosPerdidos;
                    }
                }
                catch
                {
                    return View(equipo);
                }
                return RedirectToAction(nameof(List));
            }
            return View(equipo);
        }

        // GET: Equipos/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = equipos.FirstOrDefault(e => e.EquipoId == id);
            if (equipo == null)
            {
                return NotFound();
            }

            return View(equipo);
        }

        // POST: Equipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var equipo = equipos.FirstOrDefault(e => e.EquipoId == id);
            if (equipo != null)
            {
                equipos.Remove(equipo);
            }

            return RedirectToAction(nameof(List));
        }
    }
}

