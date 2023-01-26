using ClosedXML.Excel;
using Demografi.Data;
using Demografi.Models;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Net.Mail;
using System.Data;
using GemBox.Spreadsheet;


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

        public IActionResult Generate(DateTime? from, DateTime? until, string btnsubmit)
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
                List<demografi_investor_general_saved> users = _context.demografi_investor_general_saved.Select(x => new demografi_investor_general_saved
                {

                    jumlah_sre = x.jumlah_sre,
                    jumlah_sid = x.jumlah_sid,
                    jumlah_sre_aktif_terhubung_sid = x.jumlah_sre_aktif_terhubung_sid,
                    jumlah_sre_aktif_tidak_terhubung_sid = x.jumlah_sre_aktif_tidak_terhubung_sid,
                    jumlah_sid_dengan_sre_aktif = x.jumlah_sid_dengan_sre_aktif,
                    jumlah_sid_dengan_sre_closed = x.jumlah_sid_dengan_sre_closed,
                    jumlah_sre_aktif_local = x.jumlah_sre_aktif_local,
                    jumlah_sre_aktif_asing = x.jumlah_sre_aktif_asing,
                    jumlah_sid_local = x.jumlah_sid_local,
                    jumlah_sid_asing = x.jumlah_sid_asing

                }).ToList();
                SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
                SpreadsheetInfo.FreeLimitReached += (sender, e) => e.FreeLimitReachedAction = FreeLimitReachedAction.ContinueAsTrial;
                var workbook = new ExcelFile();
                var worksheet = workbook.Worksheets.Add("DataTable to Sheet");
                var dataTable = new DataTable();

                dataTable.Columns.Add("Jumlah SRE", typeof(int));
                dataTable.Columns.Add("Jumlah SID", typeof(string));
                dataTable.Columns.Add("Jumlah SRE Aktif Terhubung SID", typeof(string));
                dataTable.Columns.Add("Jumlah SRE Aktif Tidak Terhubung SID", typeof(string));
                dataTable.Columns.Add("Jumlah SID dengan SRE Aktif", typeof(string));
                dataTable.Columns.Add("Jumlah SID dengan SRE Closed", typeof(string));
                dataTable.Columns.Add("Jumlah SRE Aktif Local", typeof(string));
                dataTable.Columns.Add("Jumlah SRE Aktif Asing", typeof(string));
                dataTable.Columns.Add("Jumlah SID Aktif Local", typeof(string));
                dataTable.Columns.Add("Jumlah SID Aktif Asing", typeof(string));

                foreach (var user in users)
                {
                dataTable.Rows.Add(new object[] { 100, user.jumlah_sre  });
                dataTable.Rows.Add(new object[] { 101, user.jumlah_sid  });
                dataTable.Rows.Add(new object[] { 102, user.jumlah_sre_aktif_terhubung_sid  });
                dataTable.Rows.Add(new object[] { 103, user.jumlah_sre_aktif_tidak_terhubung_sid  });
                dataTable.Rows.Add(new object[] { 104, user.jumlah_sid_dengan_sre_aktif  });
                dataTable.Rows.Add(new object[] { 105, user.jumlah_sid_dengan_sre_closed  });
                dataTable.Rows.Add(new object[] { 106, user.jumlah_sre_aktif_local  });
                dataTable.Rows.Add(new object[] { 107, user.jumlah_sre_aktif_asing  });
                dataTable.Rows.Add(new object[] { 108, user.jumlah_sid_local  });
                dataTable.Rows.Add(new object[] { 109, user.jumlah_sid_asing  });
                }
                worksheet.InsertDataTable(dataTable,
                new InsertDataTableOptions()
                {
                    ColumnHeaders = true,
                    StartRow = 2
                });
                workbook.Save("Generate_General.xlsx");
            }
            return View("Index", data);
        }
    }
}

