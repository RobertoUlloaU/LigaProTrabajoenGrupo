using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LigaProTrabajoenGrupo.Models;

namespace LigaProTrabajoenGrupo.Controllers
{
    public class JugadoresController : Controller
    {
        private readonly DBSqlSERVERLigaProECU _context;

        public JugadoresController(DBSqlSERVERLigaProECU context)
        {
            _context = context;
        }

        // GET: Jugadores
        public async Task<IActionResult> Index()
        {
            var dBSqlSERVERLigaProECU = _context.Jugador.Include(j => j.Equipo);
            return View(await dBSqlSERVERLigaProECU.ToListAsync());
        }

        // GET: Jugadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jugador = await _context.Jugador
                .Include(j => j.Equipo)
                .FirstOrDefaultAsync(m => m.JugadorId == id);
            if (jugador == null)
            {
                return NotFound();
            }

            return View(jugador);
        }

        // GET: Jugadores/Create
        public IActionResult Create()
        {
            ViewData["EquipoId"] = new SelectList(_context.Equipo, "EquipoId", "Nombre");
            return View();
        }

        // POST: Jugadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JugadorId,Nombre,NumeroCamiseta,Goles,Asistencias,Sueldo,EquipoId")] Jugador jugador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jugador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipoId"] = new SelectList(_context.Equipo, "EquipoId", "Nombre", jugador.EquipoId);
            return View(jugador);
        }

        // GET: Jugadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jugador = await _context.Jugador.FindAsync(id);
            if (jugador == null)
            {
                return NotFound();
            }
            ViewData["EquipoId"] = new SelectList(_context.Equipo, "EquipoId", "Nombre", jugador.EquipoId);
            return View(jugador);
        }

        // POST: Jugadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JugadorId,Nombre,NumeroCamiseta,Goles,Asistencias,Sueldo,EquipoId")] Jugador jugador)
        {
            if (id != jugador.JugadorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jugador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JugadorExists(jugador.JugadorId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipoId"] = new SelectList(_context.Equipo, "EquipoId", "Nombre", jugador.EquipoId);
            return View(jugador);
        }

        // GET: Jugadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jugador = await _context.Jugador
                .Include(j => j.Equipo)
                .FirstOrDefaultAsync(m => m.JugadorId == id);
            if (jugador == null)
            {
                return NotFound();
            }

            return View(jugador);
        }

        // POST: Jugadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jugador = await _context.Jugador.FindAsync(id);
            if (jugador != null)
            {
                _context.Jugador.Remove(jugador);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JugadorExists(int id)
        {
            return _context.Jugador.Any(e => e.JugadorId == id);
        }
    }
}
