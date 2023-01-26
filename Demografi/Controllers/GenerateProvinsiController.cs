using ClosedXML.Excel;
using Demografi.Data;
using Demografi.Models;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Net.Mail;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace Demografi.Controllers
{
    public class GenerateProvinsiController : Controller
    {
        private readonly DataContext _context;
        private IHostingEnvironment _IHosting;

        public GenerateProvinsiController(DataContext context, IHostingEnvironment IHosting)
        {
            _IHosting = IHosting;
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.demografi_investor_per_provinsi_saved);
        }

        public IActionResult GenerateProvinsi(string btnsubmit, DateTime from, DateTime until)
        {
            var data = from d in _context.demografi_investor_per_provinsi_saved select d;
            ViewData["CurrentFilter"] = from;
            ViewData["CurrentFilters"] = until;

            if (btnsubmit == "Search")
            {
                if (from != null && until != null)
                {
                    data = data.Where(d => d.Tanggal >= from && d.Tanggal <= until);
                }
            }

            if (btnsubmit == "Generate")
            {
                var date = DateTime.Now;
                var body = new BodyBuilder();
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("ihsansepriawal@gmail.com");
                    mail.To.Add("ihsandac@gmail.com");
                    mail.Subject = $"Status Generate Excel Data Demografi Investor Data Demografi Investor_{date.ToString("yyyy-MM")}";
                    mail.Body = $"Dengan Hormat,\r\n\r\nDengan ini kami informasikan bahwa per tanggal {date.ToString("dd-MM-yyyy")} proses generate Excel Data Demografi Investor dinyatakan sukses";

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


                List<demografi_investor_per_provinsi_saved> users = _context.demografi_investor_per_provinsi_saved.Select(x => new demografi_investor_per_provinsi_saved
                {
                    provinsi = x.provinsi,
                    jumlah_sid = x.jumlah_sid,

                }).ToList();
                using (XLWorkbook workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Demografi");
                    var currentRow = 1;

                    worksheet.Cell(currentRow, 1).Value = "Provinsi";
                    worksheet.Cell(currentRow, 2).Value = "Jumlah SID";

                    foreach (var user in users)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = user.provinsi;
                        worksheet.Cell(currentRow, 2).Value = user.jumlah_sid;

                    }

                    using (MemoryStream stream = new MemoryStream())
                    {
                        var location = _IHosting.WebRootPath;
                        string fileName = $"Generate_Provinsi.xlsx";
                        Response.AddHeader("content-disposition", "attachment;  filename=" + fileName);
                        var Output = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\generate", fileName);
                        workbook.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Generate_Provinsi.xlsx");
                    }
                }

               
            }
             return View("Index", data);
        }
    }
}
