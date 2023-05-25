using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MarcoEKarol.Models;

namespace MarcoEKarol.Controllers
{
    public class ListaDePresentesController : Controller
    {
        private readonly MyDbContext _context;

        public ListaDePresentesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: ListaDePresentes
        public async Task<IActionResult> Index()
        {
              return _context.ListaDePresentes != null ? 
                          View(await _context.ListaDePresentes.ToListAsync()) :
                          Problem("Entity set 'MyDbContext.ListaDePresentes'  is null.");
        }

        // GET: ListaDePresentes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ListaDePresentes == null)
            {
                return NotFound();
            }

            var listaDePresentes = await _context.ListaDePresentes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listaDePresentes == null)
            {
                return NotFound();
            }

            return View(listaDePresentes);
        }

        // GET: ListaDePresentes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ListaDePresentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImageData,Pryce")] ListaDePresentes listaDePresentes, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await image.CopyToAsync(memoryStream);
                        // Convert the image to a byte array and save it
                        listaDePresentes.ImageData = memoryStream.ToArray();
                    }
                }
                _context.Add(listaDePresentes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(listaDePresentes);
        }

        // GET: ListaDePresentes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ListaDePresentes == null)
            {
                return NotFound();
            }

            var listaDePresentes = await _context.ListaDePresentes.FindAsync(id);
            if (listaDePresentes == null)
            {
                return NotFound();
            }
            return View(listaDePresentes);
        }

        // POST: ListaDePresentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImageData,Pryce")] ListaDePresentes listaDePresentes)
        {
            if (id != listaDePresentes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listaDePresentes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListaDePresentesExists(listaDePresentes.Id))
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
            return View(listaDePresentes);
        }

        // GET: ListaDePresentes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ListaDePresentes == null)
            {
                return NotFound();
            }

            var listaDePresentes = await _context.ListaDePresentes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listaDePresentes == null)
            {
                return NotFound();
            }

            return View(listaDePresentes);
        }

        // POST: ListaDePresentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ListaDePresentes == null)
            {
                return Problem("Entity set 'MyDbContext.ListaDePresentes'  is null.");
            }
            var listaDePresentes = await _context.ListaDePresentes.FindAsync(id);
            if (listaDePresentes != null)
            {
                _context.ListaDePresentes.Remove(listaDePresentes);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListaDePresentesExists(int id)
        {
          return (_context.ListaDePresentes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
