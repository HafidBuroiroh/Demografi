using ClosedXML.Excel;
using Demografi.Data;
using Demografi.Models;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Net.Mail;
using System.Data;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Demografi.Controllers
{
    public class GenerateController : Controller
    {
        private readonly DataContext _context;
        private IHostingEnvironment _IHosting;
        public GenerateController(DataContext context, IHostingEnvironment iHosting)
        {
            _context = context;
            _IHosting = iHosting;
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
                    jumlah_sid_asing = x.jumlah_sid_asing,
                }).ToList();

                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Demografi");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "Jumlah SRE";
                worksheet.Cell(currentRow, 2).Value = "Jumlah SID";
                worksheet.Cell(currentRow, 3).Value = "Jumlah SRE Terhubung SID";
                worksheet.Cell(currentRow, 4).Value = "Jumlah SRE Aktif Tidak Terhubung SID";
                worksheet.Cell(currentRow, 5).Value = "Jumlah SID Dengan SRE Aktif";
                worksheet.Cell(currentRow, 6).Value = "Jumlah SID Dengan SRE Closed";
                worksheet.Cell(currentRow, 7).Value = "Jumlah SRE Aktif Local";
                worksheet.Cell(currentRow, 8).Value = "Jumlah SRE Aktif Asing";
                worksheet.Cell(currentRow, 9).Value = "Jumlah SID Local";
                worksheet.Cell(currentRow, 10).Value = "Jumlah SID Asing";

                foreach (var user in users)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = user.jumlah_sre;
                    worksheet.Cell(currentRow, 2).Value = user.jumlah_sid;
                    worksheet.Cell(currentRow, 3).Value = user.jumlah_sre_aktif_terhubung_sid;
                    worksheet.Cell(currentRow, 4).Value = user.jumlah_sre_aktif_tidak_terhubung_sid;
                    worksheet.Cell(currentRow, 5).Value = user.jumlah_sid_dengan_sre_aktif;
                    worksheet.Cell(currentRow, 6).Value = user.jumlah_sid_dengan_sre_closed;
                    worksheet.Cell(currentRow, 7).Value = user.jumlah_sre_aktif_local;
                    worksheet.Cell(currentRow, 8).Value = user.jumlah_sre_aktif_asing;
                    worksheet.Cell(currentRow, 9).Value = user.jumlah_sid_local;
                    worksheet.Cell(currentRow, 10).Value = user.jumlah_sid_asing;


                }
                var location = _IHosting.WebRootPath + "/Text/";
                var filename = location + "Data_General.xlsx";
                using var stream = new MemoryStream();
                var content = stream.ToArray();
                workbook.SaveAs(filename);

                var date = DateTime.Now;
                var body = new BodyBuilder();
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("ihsansepriawal@gmail.com");
                    mail.To.Add("ihsandac@gmail.com");
                    mail.Subject = $"Status Generate Excel Data Demografi Investor Data Demografi Investor_{date.ToString("yyyy-MM")}";
                    mail.Body = $"Dengan Hormat,\r\n\r\nDengan ini kami informasikan bahwa per tanggal {date.ToString("dd-MM-yyyy")} proses generate Excel Data Demografi Investor dinyatakan sukses";
                    DirectoryInfo dir = new DirectoryInfo(location);
                    foreach (FileInfo file in dir.GetFiles("."))
                    {
                        if (file.Exists)
                        {
                            mail.Attachments.Add(new Attachment(filename));
                        }
                    }

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.EnableSsl = true;
                        smtp.Credentials = new System.Net.NetworkCredential("ihsansepriawal@gmail.com", "dnaxriscszcfyljt");
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.Send(mail);
                    };
                }

            }
            return View("Index", data);
        }
    }
}

