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
    public class KolOtklPofidernoController : Controller
    {
        private readonly GridContext _context;

        public KolOtklPofidernoController(GridContext context)
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
  
        public async Task<IActionResult> ExportToExcel(DateTime startdate, DateTime enddate)
        {
            /* var gridContext = _context.Outages
                   .Include(o => o.CasesOfOutage).Include(o => o.Feeder).Include(o => o.RES)
                   .Include(o => o.Substation).OrderByDescending(o => o.Date_outage);

              Outage trew = new Outage();
              gridContext = (IOrderedQueryable<Outage>)gridContext.Where(x => x.Date_outage >= startdate && x.Date_outage <= enddate);
            */
            var gridContext = _context.Outages;
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Students");
                var currentRow = 1;
                #region Header
                worksheet.Cell(currentRow, 1).Value = "РЭС";
                worksheet.Cell(currentRow, 2).Value = "Подстанция";
                worksheet.Cell(currentRow, 3).Value = "Номер фидера";
                worksheet.Cell(currentRow, 4).Value = "Количество отключений";
                worksheet.Cell(currentRow, 5).Value = "Дата отключения";
                #endregion

                #region Body
                foreach (var item in gridContext)
                {
                    currentRow++;

                    worksheet.Cell(currentRow, 1).Value = item.RESID;
                    worksheet.Cell(currentRow, 2).Value = item.SubstationID;
                    worksheet.Cell(currentRow, 3).Value = item.FeederID;
                    worksheet.Cell(currentRow, 4).Value = _context.Outages.Count(x => x.FeederID == item.FeederID);
                    worksheet.Cell(currentRow, 5).Value = item.Date_outage;


                }
                #endregion
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                         content,
                         "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                         "Students.xlsx"
                         );
                }
            }
        }
    }
}