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
    public class FormularioPartidosController : Controller
    {
        private readonly DBSqlSERVERLigaProECU _context;

        public FormularioPartidosController(DBSqlSERVERLigaProECU context)
        {
            _context = context;
        }

        // GET: FormularioPartidos
        public async Task<IActionResult> Index()
        {
            var dBSqlSERVERLigaProECU = _context.FormularioPartido.Include(f => f.Equipo);
            return View(await dBSqlSERVERLigaProECU.ToListAsync());
        }

        // GET: FormularioPartidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formularioPartido = await _context.FormularioPartido
                .Include(f => f.Equipo)
                .FirstOrDefaultAsync(m => m.FormularioPartidoId == id);
            if (formularioPartido == null)
            {
                return NotFound();
            }

            return View(formularioPartido);
        }

        // GET: FormularioPartidos/Create
        public IActionResult Create()
        {
            ViewData["EquipoId"] = new SelectList(_context.Equipo, "EquipoId", "Nombre");
            return View();
        }

        // POST: FormularioPartidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FormularioPartidoId,NombreEquipo,PartidosJugados,PartidosGanados,PartidosEmpatados,PartidosPerdidos,EquipoId")] FormularioPartido formularioPartido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formularioPartido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipoId"] = new SelectList(_context.Equipo, "EquipoId", "Nombre", formularioPartido.EquipoId);
            return View(formularioPartido);
        }

        // GET: FormularioPartidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formularioPartido = await _context.FormularioPartido.FindAsync(id);
            if (formularioPartido == null)
            {
                return NotFound();
            }
            ViewData["EquipoId"] = new SelectList(_context.Equipo, "EquipoId", "Nombre", formularioPartido.EquipoId);
            return View(formularioPartido);
        }

        // POST: FormularioPartidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FormularioPartidoId,NombreEquipo,PartidosJugados,PartidosGanados,PartidosEmpatados,PartidosPerdidos,EquipoId")] FormularioPartido formularioPartido)
        {
            if (id != formularioPartido.FormularioPartidoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formularioPartido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormularioPartidoExists(formularioPartido.FormularioPartidoId))
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
            ViewData["EquipoId"] = new SelectList(_context.Equipo, "EquipoId", "Nombre", formularioPartido.EquipoId);
            return View(formularioPartido);
        }

        // GET: FormularioPartidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formularioPartido = await _context.FormularioPartido
                .Include(f => f.Equipo)
                .FirstOrDefaultAsync(m => m.FormularioPartidoId == id);
            if (formularioPartido == null)
            {
                return NotFound();
            }

            return View(formularioPartido);
        }

        // POST: FormularioPartidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var formularioPartido = await _context.FormularioPartido.FindAsync(id);
            if (formularioPartido != null)
            {
                _context.FormularioPartido.Remove(formularioPartido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormularioPartidoExists(int id)
        {
            return _context.FormularioPartido.Any(e => e.FormularioPartidoId == id);
        }
    }
}
