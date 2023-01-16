using ClosedXML.Excel;
using Demografi.Data;
using Demografi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demografi.Controllers
{
    public class GenerateUsiaController : Controller
    {
        private readonly DataContext _context;
        public GenerateUsiaController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.demografi_investor_per_usia_saved.ToList());
        }

        public IActionResult GenerateUsia(string btnsubmit, DateTime from, DateTime until)
        {

            var data = from d in _context.demografi_investor_per_usia_saved select d;
            ViewData["CurrentFilter"] = from;
            ViewData["CurrentFilters"] = until;

            if (btnsubmit == "Search")
            {
                if (from != null && until != null)
                {
                    data = data.Where(d => d.tanggal >= from && d.tanggal <= until);
                }
            }

            if (btnsubmit == "Generate")
            {
                List<demografi_investor_per_usia_saved> users = _context.demografi_investor_per_usia_saved.Select(x => new demografi_investor_per_usia_saved
                {
                    Usia = x.Usia,
                    jumlah_sid = x.jumlah_sid
                }
                ).ToList();

                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Demografi");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "Usia";
                worksheet.Cell(currentRow, 2).Value = "Jumlah SID";

                foreach (var user in users)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = user.Usia;
                    worksheet.Cell(currentRow, 2).Value = user.jumlah_sid;

                }
                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                var content = stream.ToArray();

                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Generate_Usia.xlsx");
            }
            return View("index", data);
        }
    }
}
