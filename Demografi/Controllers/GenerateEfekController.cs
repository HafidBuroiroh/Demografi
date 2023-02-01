using ClosedXML.Excel;
using Demografi.Data;
using Demografi.Models;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Net.Mail;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Demografi.Controllers
{
    public class GenerateEfekController : Controller
    {
        private readonly DataContext _context;
        private IHostingEnvironment _IHosting;
        public GenerateEfekController(DataContext context, IHostingEnvironment IHosting)
        {
            _context = context;
            _IHosting = IHosting;

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
                   
                   Tipe_efek= x.Tipe_efek,
                   jumlah_sid= x.jumlah_sid,
                   jumlah_sre= x.jumlah_sre,
                }).ToList();

                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Demografi Tipe Efek");
                var currentRow = 1;

               
                worksheet.Cell(currentRow, 1).Value = "Tipe Efek";
                worksheet.Cell(currentRow, 2).Value = "Jumlah SID";
                worksheet.Cell(currentRow, 3).Value = "Jumlah SRE";

                foreach (var user in users)
                {
                    currentRow++;
                   
                    worksheet.Cell(currentRow, 1).Value = user.Tipe_efek;
                    worksheet.Cell(currentRow, 2).Value = user.jumlah_sid;
                    worksheet.Cell(currentRow, 3).Value = user.jumlah_sre;
                }

                var location = _IHosting.WebRootPath + "/Text/";
                var filename = location + "Data_Efek.xlsx";
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
