using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GridCom.Data;
using GridCom.Models;
using ClosedXML.Excel;
using System.IO;
using System.Collections;
using DocumentFormat.OpenXml.Drawing.Charts;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Html;

namespace GridCom.Controllers
{
    public class CaseOtklController:Controller
    {
        private readonly GridContext _context;

        public CaseOtklController(GridContext context)
        {
            _context = context;
        }

        // GET: Outage
        public async Task<IActionResult> Index(string searchString, DateTime startdate, DateTime enddate)
        {
            ViewData["CurrentFilter"] = searchString;

            var gridContext = _context.Outages
                .Include(o => o.CasesOfOutage).Include(o => o.Feeder).Include(o => o.RES)
                .Include(o => o.Substation).OrderByDescending(o => o.Date_outage);

            gridContext = (IOrderedQueryable<Outage>)gridContext.Where(x => x.Date_outage >= startdate && x.Date_outage <= enddate);
            return View(await gridContext.ToListAsync());

        }
    }
}
