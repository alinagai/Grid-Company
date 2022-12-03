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
    public class SubstationController : Controller
    {
        private readonly GridContext _context;

        public SubstationController(GridContext context)
        {
            _context = context;
        }

        // GET: Substation
        public async Task<IActionResult> Index()
        {
            var gridContext = _context.Substations.Include(s => s.RES);
            return View(await gridContext.ToListAsync());
        }

        // GET: Substation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var substation = await _context.Substations
                .Include(s => s.RES)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.SubstationID == id);
            if (substation == null)
            {
                return NotFound();
            }

            return View(substation);
        }

        // GET: Substation/Create
        public IActionResult Create()
        {
            // ViewData["RESID"] = new SelectList(_context.RES, "RESID", "RESID");
            PopulateDepartmentsDropDownList();
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubstationID,Name_PS,RESID")] Substation substation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(substation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateDepartmentsDropDownList(substation.RESID);
            return View(substation);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var substation = await _context.Substations
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.SubstationID == id);
            if (substation== null)
            {
                return NotFound();
            }
            PopulateDepartmentsDropDownList(substation.RESID);
            return View(substation);
        }

   
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var substationToUpdate = await _context.Substations
                .FirstOrDefaultAsync(c => c.SubstationID == id);

            if (await TryUpdateModelAsync<Substation>(substationToUpdate,
                "",
                c => c.Name_PS, c => c.RESID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            PopulateDepartmentsDropDownList(substationToUpdate.RESID);
            return View(substationToUpdate);
        }
        private void PopulateDepartmentsDropDownList(object selectedRES = null)
        {
            var resQuery = from r in _context.RES
                                   orderby r.Name_RES
                                   select r;
            ViewBag.RESID = new SelectList(resQuery.AsNoTracking(), "RESID", "Name_RES", selectedRES);
        }

        // GET: Substation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var substation = await _context.Substations
                .Include(s => s.RES)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.SubstationID == id);
            if (substation == null)
            {
                return NotFound();
            }

            return View(substation);
        }

        // POST: Substation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var substation = await _context.Substations.FindAsync(id);
            _context.Substations.Remove(substation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubstationExists(int id)
        {
            return _context.Substations.Any(e => e.SubstationID == id);
        }
    }
}
