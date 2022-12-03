using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GridCom.Data;
using GridCom.Models;

namespace GridCom.Controllers
{
    public class RESController : Controller
    {
        private readonly GridContext _context;

        public RESController(GridContext context)
        {
            _context = context;
        }

        // GET: RES
        public async Task<IActionResult> Index()
        {
            return View(await _context.RES.ToListAsync());
        }

        // GET: RES/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rES = await _context.RES
                .FirstOrDefaultAsync(m => m.RESID == id);
            if (rES == null)
            {
                return NotFound();
            }

            return View(rES);
        }

        // GET: RES/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RES/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RESID,Name_RES,FullName_RES,Adress_RES")] RES rES)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rES);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rES);
        }

        // GET: RES/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rES = await _context.RES.FindAsync(id);
            if (rES == null)
            {
                return NotFound();
            }
            return View(rES);
        }

        // POST: RES/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RESID,Name_RES,FullName_RES,Adress_RES")] RES rES)
        {
            if (id != rES.RESID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rES);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RESExists(rES.RESID))
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
            return View(rES);
        }

        // GET: RES/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rES = await _context.RES
                .FirstOrDefaultAsync(m => m.RESID == id);
            if (rES == null)
            {
                return NotFound();
            }

            return View(rES);
        }

        // POST: RES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rES = await _context.RES.FindAsync(id);
            _context.RES.Remove(rES);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RESExists(int id)
        {
            return _context.RES.Any(e => e.RESID == id);
        }
    }
}
