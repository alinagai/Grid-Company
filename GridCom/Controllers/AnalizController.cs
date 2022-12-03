using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GridCom.Data;
using GridCom.Models;
using OfficeOpenXml;
using Microsoft.AspNetCore.Http;

namespace GridCom.Controllers
{

    public class AnalizController : Controller
    {

        private readonly GridContext _context;

        public AnalizController(GridContext context)
        {
            _context = context;
        }
   
        // GET: Outage
         public async Task<IActionResult> Index(string searchString, int? pageNumber, DateTime startdate, DateTime enddate)
         {
             ViewData["CurrentFilter"] = searchString;

             var gridContext = _context.Outages
                 .Include(o => o.CasesOfOutage).Include(o => o.Feeder).Include(o => o.RES)
                 .Include(o => o.Substation).OrderByDescending(o => o.Date_outage);
         
          
                 gridContext = (IOrderedQueryable<Outage>)gridContext.Where(x=>x.Date_outage>=startdate&&x.Date_outage<=enddate);
             
             int pageSize = 3;
             return View(await PaginatedList<Outage>.CreateAsync(gridContext.AsNoTracking(), pageNumber ?? 1, pageSize));
         }
       public void ExportToExcel(DateTime startdate, DateTime enddate)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var gridContext = _context.Outages
                .Include(o => o.CasesOfOutage).Include(o => o.Feeder).Include(o => o.RES)
                .Include(o => o.Substation).OrderByDescending(o => o.Date_outage);


            gridContext = (IOrderedQueryable<Outage>)gridContext.Where(x => x.Date_outage >= startdate && x.Date_outage <= enddate).Distinct();
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");
            ws.Cells["A1"].Value = "RES";
            ws.Cells["B1"].Value = "Substation";
            ws.Cells["C1"].Value = "Feeder";
            ws.Cells["D1"].Value = "Extent of Feeder";

            ws.Cells["E1"].Value = "Date of outage";
            ws.Cells["F1"].Value = "Downtime";
            int rowStart = 2;
            foreach(var item in gridContext)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.RES.Name_RES;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Substation.Name_PS;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.Feeder.Num_feeder;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Feeder.Extent_feeder;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.Date_outage;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.Downtime;
                rowStart++;
            }
            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        
            
        }
    }

}