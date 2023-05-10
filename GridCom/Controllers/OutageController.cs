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
    public class OutageController : Controller
    {
     
        private readonly GridContext _context;

        public OutageController(GridContext context)
        {
            _context = context;
        }
   
        // GET: Outage
        public async Task<IActionResult> Index(string searchString, int? pageNumber)
        {
            ViewData["CurrentFilter"] = searchString;

            var gridContext = _context.Outages
                .Include(o => o.CasesOfOutage).Include(o => o.Feeder).Include(o => o.RES)
                .Include(o => o.Substation).OrderByDescending(o => o.Date_outage);

            if (searchString != null)
            {
                pageNumber = 1;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                gridContext = (IOrderedQueryable<Outage>)gridContext.Where(s => s.RES.Name_RES.Contains(searchString));
            }
            int pageSize = 3;
            return View(await PaginatedList<Outage>.CreateAsync(gridContext.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

       

        // GET: Outage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outage = await _context.Outages
                .Include(o => o.CasesOfOutage)
                .Include(o => o.Feeder)
                .Include(o => o.RES)
                .Include(o => o.Substation)
                .FirstOrDefaultAsync(m => m.OutageID == id);
            if (outage == null)
            {
                return NotFound();
            }

            return View(outage);
        }
              public IActionResult Create()         
              {

                  ViewBag.RESList = new SelectList(GetRESList(),"RESID", "Name_RES");

                  PopulateCasesDropDownList();
                  //  PopulateCasesDropDownList();
                  return View();
              }
              public List <RES> GetRESList()
              {
                  List<RES> res = _context.RES.ToList();
                  return res;
              }
              public ActionResult GetSubstationList(int RESID)
              {
                  List<Substation> selectlist = _context.Substations.Where(x => x.RESID == RESID).ToList();
                  ViewBag.SubList = new SelectList(selectlist, "SubstationID", "Name_PS");
                  return PartialView("DisplaySubs");
              }


        public ActionResult GetFeederList(int SubstationID)
        {
            List<Feeder> selectlist = _context.Feeders.Where(x => x.SubstationID == SubstationID).ToList();
            ViewBag.FeederList = new SelectList(selectlist, "FeederID", "Num_feeder");
            return PartialView("DisplayFeeder");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OutageID,RESID,SubstationID,FeederID,"
               + "Date_outage,Downtime,UnderSupply,CasesOfOutageID,APV,RPV,OZZ")] Outage outage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(outage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }



            PopulateCasesDropDownList(outage.CasesOfOutageID);
            return View(outage);
        }
    
          
            public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outage = await _context.Outages
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.OutageID == id);
            if (outage == null)
            {
                return NotFound();
            }
            ViewBag.RESList = new SelectList(GetRESList(), "RESID", "Name_RES");
            PopulateCasesDropDownList(outage.CasesOfOutageID);
            return View(outage);
        }
  
 
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outageToUpdate = await _context.Outages
                .FirstOrDefaultAsync(c => c.OutageID == id);

            if (await TryUpdateModelAsync<Outage>(outageToUpdate,
                "",
                c => c.RESID, c => c.SubstationID, c => c.FeederID, c => c.Date_outage, c => c.Downtime,
                c => c.UnderSupply, c => c.CasesOfOutageID,
                c => c.APV, c => c.RPV, c => c.OZZ))
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
           /* PopulateRESDropDownList(outageToUpdate.RESID);
            PopulateSubstationDropDownList(outageToUpdate.SubstationID);
            PopulateFeederDropDownList(outageToUpdate.FeederID);*/
            PopulateCasesDropDownList(outageToUpdate.CasesOfOutageID);

            return View(outageToUpdate);
        }
        private void PopulateRESDropDownList(object selectedRES = null)
        {
            var RESQuery = from d in _context.RES
                           orderby d.Name_RES
                           select d;
            ViewBag.RESID = new SelectList(RESQuery.AsNoTracking(), "RESID", "Name_RES", selectedRES);
        }
        private void PopulateSubstationDropDownList(object selectedSub=null)
        {
           
            var varik = from x in _context.RES
                        select x.RESID;
            string intid = varik.ToString();

            var SubQuery = from d in _context.Substations
                           join c in _context.RES on d.RESID equals c.RESID
                           where d.RESID == c.RESID
                           orderby d.Name_PS
                           select d;
            ViewBag.SubstationID = new SelectList(SubQuery.AsNoTracking(), "SubstationID", "Name_PS", selectedSub);
        }
        private void PopulateFeederDropDownList(object selectedFed = null)
        {
            var FedQuery = from d in _context.Feeders
                           orderby d.Num_feeder
                           select d;
            ViewBag.FeederID = new SelectList(FedQuery.AsNoTracking(), "FeederID", "Num_feeder", selectedFed); 
        }
        private void PopulateCasesDropDownList(object selectedCase = null)
        {
            var caseQuery = from d in _context.CasesOfOutage
                           orderby d.Case_outage
                           select d;
            ViewBag.CasesOfOutageID = new SelectList(caseQuery.AsNoTracking(), "CasesOfOutageID", "Case_outage", selectedCase);
        }

        // GET: Outage/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outage = await _context.Outages
                .Include(o => o.CasesOfOutage)
                .Include(o => o.Feeder)
                .Include(o => o.RES)
                .Include(o => o.Substation)
                .FirstOrDefaultAsync(m => m.OutageID == id);
            if (outage == null)
            {
                return NotFound();
            }

            return View(outage);
        }

        // POST: Outage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var outage = await _context.Outages.FindAsync(id);
            _context.Outages.Remove(outage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OutageExists(int id)
        {
            return _context.Outages.Any(e => e.OutageID == id);
        }
    }
}
