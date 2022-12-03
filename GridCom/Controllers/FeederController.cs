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
    public class FeederController : Controller
    {
        private readonly GridContext _context;

        public FeederController(GridContext context)
        {
            _context = context;
        }

        // GET: Feeder
        public async Task<IActionResult> Index()
        {
            var gridContext = _context.Feeders.Include(f => f.Substations);
            return View(await gridContext.ToListAsync());
        }

        // GET: Feeder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feeder = await _context.Feeders
                .Include(f => f.Substations)
                 .AsNoTracking()
                .FirstOrDefaultAsync(m => m.FeederID == id);
            if (feeder == null)
            {
                return NotFound();
            }

            return View(feeder);
        }
        public IActionResult Create()
        {
            PopulateDepartmentsDropDownList();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FeederID,SubstationID,Num_feeder,Extent_feeder")]Feeder feeder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feeder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateDepartmentsDropDownList(feeder.SubstationID);
            return View(feeder);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feeder = await _context.Feeders
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.FeederID == id);
            if (feeder == null)
            {
                return NotFound();
            }
            PopulateDepartmentsDropDownList(feeder.FeederID);
            return View(feeder);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feederToUpdate = await _context.Feeders
                .FirstOrDefaultAsync(c => c.FeederID == id);

            if (await TryUpdateModelAsync<Feeder>(feederToUpdate,
                "",
                c => c.SubstationID, c => c.Num_feeder, c => c.Extent_feeder))
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
            PopulateDepartmentsDropDownList(feederToUpdate.SubstationID);
            return View(feederToUpdate);
        }
        private void PopulateDepartmentsDropDownList(object selectedSubstation = null)
        {
            var subsQuery = from d in _context.Substations
                                   orderby d.Name_PS
                                   select d;
            ViewBag.SubstationID = new SelectList(subsQuery.AsNoTracking(), "SubstationID", "Name_PS", selectedSubstation);
        }
        // GET: Feeder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feeder = await _context.Feeders
                .Include(f => f.Substations)
                 .AsNoTracking()
                .FirstOrDefaultAsync(m => m.FeederID == id);
            if (feeder == null)
            {
                return NotFound();
            }

            return View(feeder);
        }

        // POST: Feeder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feeder = await _context.Feeders.FindAsync(id);
            _context.Feeders.Remove(feeder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeederExists(int id)
        {
            return _context.Feeders.Any(e => e.FeederID == id);
        }
    }
}
