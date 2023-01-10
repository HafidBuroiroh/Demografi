using ClosedXML.Excel;
using Demografi.Data;
using Demografi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demografi.Controllers
{
    public class GenerateController : Controller
    {
        private readonly DataContext _context;
        public GenerateController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.demografi_investor_general_saved.ToList());
        }

        public IActionResult Generate(DateTime from, DateTime until, string btnsubmit)
        {
            var data = from d in _context.demografi_investor_general_saved select d;
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
                if (from != null && until != null)
                {
                    data = data.Where(d => d.tanggal >= from && d.tanggal <= until);

                    List<demografi_investor_general_saved> users = _context.demografi_investor_general_saved.Select(x => new demografi_investor_general_saved
                    {
                        demografi_investor_general_id = x.demografi_investor_general_id,
                        tanggal = x.tanggal,
                        Periode = x.Periode,
                        jumlah_sre = x.jumlah_sre,
                        jumlah_sid = x.jumlah_sid,
                        jumlah_sre_aktif_terhubung_sid = x.jumlah_sre_aktif_terhubung_sid,
                        jumlah_sre_aktif_tidak_terhubung_sid = x.jumlah_sre_aktif_tidak_terhubung_sid,
                        jumlah_sid_dengan_sre_aktif = x.jumlah_sid_dengan_sre_aktif,
                        jumlah_sid_dengan_sre_closed = x.jumlah_sid_dengan_sre_closed,
                        jumlah_sre_aktif_local = x.jumlah_sre_aktif_local,
                        jumlah_sre_aktif_asing = x.jumlah_sre_aktif_asing,
                        jumlah_sid_local = x.jumlah_sid_local,
                        jumlah_sid_asing = x.jumlah_sid_asing,
                    }).ToList();

                    using var workbook = new XLWorkbook();
                    var worksheet = workbook.Worksheets.Add("Demografi");
                    var currentRow = 1;

                    worksheet.Cell(currentRow, 1).Value = "Demografi Investor General ID";
                    worksheet.Cell(currentRow, 2).Value = "Tanggal";
                    worksheet.Cell(currentRow, 3).Value = "Periode";
                    worksheet.Cell(currentRow, 4).Value = "Jumlah SRE";
                    worksheet.Cell(currentRow, 5).Value = "Jumlah SID";
                    worksheet.Cell(currentRow, 6).Value = "Jumlah SRE Terhubung SID";
                    worksheet.Cell(currentRow, 7).Value = "Jumlah SRE Aktif Tidak Terhubung SID";
                    worksheet.Cell(currentRow, 8).Value = "Jumlah SID Dengan SRE Aktif";
                    worksheet.Cell(currentRow, 9).Value = "Jumlah SID Dengan SRE Closed";
                    worksheet.Cell(currentRow, 10).Value = "Jumlah SRE Aktif Local";
                    worksheet.Cell(currentRow, 11).Value = "Jumlah SRE Aktif Asing";
                    worksheet.Cell(currentRow, 12).Value = "Jumlah SID Local";
                    worksheet.Cell(currentRow, 13).Value = "Jumlah SID Asing";

                    foreach (var user in users)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = user.demografi_investor_general_id;
                        worksheet.Cell(currentRow, 2).Value = user.tanggal;
                        worksheet.Cell(currentRow, 3).Value = user.Periode;
                        worksheet.Cell(currentRow, 4).Value = user.jumlah_sre;
                        worksheet.Cell(currentRow, 5).Value = user.jumlah_sid;
                        worksheet.Cell(currentRow, 6).Value = user.jumlah_sre_aktif_terhubung_sid;
                        worksheet.Cell(currentRow, 7).Value = user.jumlah_sre_aktif_tidak_terhubung_sid;
                        worksheet.Cell(currentRow, 8).Value = user.jumlah_sid_dengan_sre_aktif;
                        worksheet.Cell(currentRow, 9).Value = user.jumlah_sid_dengan_sre_closed;
                        worksheet.Cell(currentRow, 10).Value = user.jumlah_sre_aktif_local;
                        worksheet.Cell(currentRow, 11).Value = user.jumlah_sre_aktif_asing;
                        worksheet.Cell(currentRow, 12).Value = user.jumlah_sid_local;
                        worksheet.Cell(currentRow, 13).Value = user.jumlah_sid_asing;
                    }

                    using var stream = new MemoryStream();
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "users.xlsx");

                }
            }
            return View("Index", data);
        }
    }
}
