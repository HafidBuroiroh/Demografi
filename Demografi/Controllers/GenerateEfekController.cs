using ClosedXML.Excel;
using Demografi.Data;
using Demografi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demografi.Controllers
{
    public class GenerateEfekController : Controller
    {
        private readonly DataContext _context;
        public GenerateEfekController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.demografi_investor_per_tipe_efek_saved.ToList());
        }

        public IActionResult GenerateEfek(string btnsubmit, DateTime from, DateTime until)
        {
            var data = from d in _context.demografi_investor_per_tipe_efek_saved select d;
            ViewData["CurrentFilter"] = from;
            ViewData["CurrentFilters"] = until;

            if (btnsubmit == "Search")
            {

                if (from != null && until != null)
                {
                    data = data.Where(d => d.tanggal >= from && d.tanggal <= until);
                }
            }

            if(btnsubmit == "GenerateEfek")
            {
                List<demografi_investor_per_tipe_efek_saved> users = _context.demografi_investor_per_tipe_efek_saved.Select(x => new demografi_investor_per_tipe_efek_saved
                {
                   tanggal = x.tanggal,
                   Periode= x.Periode,
                   Tipe_efek= x.Tipe_efek,
                   jumlah_sid= x.jumlah_sid,
                   jumlah_sre= x.jumlah_sre,
                }).ToList();

                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Demografi Tipe Efek");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "Tanggal";
                worksheet.Cell(currentRow, 2).Value = "Periode";
                worksheet.Cell(currentRow, 3).Value = "Tipe Efek";
                worksheet.Cell(currentRow, 4).Value = "Jumlah SID";
                worksheet.Cell(currentRow, 5).Value = "Jumlah SRE";

                foreach (var user in users)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = user.tanggal;
                    worksheet.Cell(currentRow, 2).Value = user.Periode;
                    worksheet.Cell(currentRow, 3).Value = user.Tipe_efek;
                    worksheet.Cell(currentRow, 4).Value = user.jumlah_sid;
                    worksheet.Cell(currentRow, 5).Value = user.jumlah_sre;
                }

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                var content = stream.ToArray();

                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "demografi_tipe_efek.xlsx");

            }
            return View("Index", data);
        }
    }
}
