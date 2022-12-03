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
    public class CaseController : Controller
    {
        private readonly GridContext _context;

        public CaseController(GridContext context)
        {
            _context = context;
        }

        // GET: Case
        public async Task<IActionResult> Index()
        {
            return View(await _context.CasesOfOutage.ToListAsync());
        }

        // GET: Case/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var casesOfOutage = await _context.CasesOfOutage
                .FirstOrDefaultAsync(m => m.CasesOfOutageID == id);
            if (casesOfOutage == null)
            {
                return NotFound();
            }

            return View(casesOfOutage);
        }

        // GET: Case/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Case/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CasesOfOutageID,Case_outage")] CasesOfOutage casesOfOutage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(casesOfOutage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(casesOfOutage);
        }

        // GET: Case/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var casesOfOutage = await _context.CasesOfOutage.FindAsync(id);
            if (casesOfOutage == null)
            {
                return NotFound();
            }
            return View(casesOfOutage);
        }

        // POST: Case/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CasesOfOutageID,Case_outage")] CasesOfOutage casesOfOutage)
        {
            if (id != casesOfOutage.CasesOfOutageID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(casesOfOutage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CasesOfOutageExists(casesOfOutage.CasesOfOutageID))
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
            return View(casesOfOutage);
        }

        // GET: Case/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var casesOfOutage = await _context.CasesOfOutage
                .FirstOrDefaultAsync(m => m.CasesOfOutageID == id);
            if (casesOfOutage == null)
            {
                return NotFound();
            }

            return View(casesOfOutage);
        }

        // POST: Case/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var casesOfOutage = await _context.CasesOfOutage.FindAsync(id);
            _context.CasesOfOutage.Remove(casesOfOutage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CasesOfOutageExists(int id)
        {
            return _context.CasesOfOutage.Any(e => e.CasesOfOutageID == id);
        }
    }
}
