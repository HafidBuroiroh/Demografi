using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Demografi.Models;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
using SqlConnection = Microsoft.Data.SqlClient.SqlConnection;
using SqlCommand = Microsoft.Data.SqlClient.SqlCommand;
using SqlDataReader = Microsoft.Data.SqlClient.SqlDataReader;
using SqlBulkCopy = Microsoft.Data.SqlClient.SqlBulkCopy;
using Demografi.Data;
using System.Data.Entity;
using DocumentFormat.OpenXml.InkML;

namespace Demografi.Controllers
{
    public class TransferDataProvinsiController : Controller
    {
        private DateTime? DateFrom;
        private DateTime? DateUntil;
        private readonly DataContext _context;
        public IConfiguration _Configuration { get; set; }
        public TransferDataProvinsiController(IConfiguration configuration, DataContext context)
        {
            _Configuration = configuration;
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.demografi_investor_per_provinsi.ToList());
        }
        public IActionResult Transfer(DateTime from, DateTime until, string btnsubmit)
        {
            var data = from d in _context.demografi_investor_per_provinsi select d;
            ViewData["CurrentFIlter"] = from;
            ViewData["CurrentFilters"] = until;

            if (btnsubmit == "Search")
            {
                if (from != null && until != null)
                {
                    data = data.Where(d => d.Tanggal >= from && d.Tanggal <= until);
                }
            }
            if (btnsubmit == "Transfer")
            {
                var connectionstring = _Configuration["ConnectionStrings:DefaultConnection"];
                SqlConnection con = new SqlConnection(connectionstring);
                var sql = $"SELECT * FROM demografi_investor_per_provinsi WHERE Tanggal Between '{from.ToString("yyyy-MM-dd")}' AND '{until.ToString("yyyy-MM-dd")}'";
                SqlCommand com = new SqlCommand(sql, con);
                con.Open();

                SqlDataReader dataReader = com.ExecuteReader();

                SqlBulkCopy sqlBulk = new SqlBulkCopy(con);
                sqlBulk.DestinationTableName = "dbo.demografi_investor_per_provinsi_saved";

                sqlBulk.ColumnMappings.Add("demografi_investor_per_provinsi_id", "demografi_investor_per_provinsi_id");
                sqlBulk.ColumnMappings.Add("Tanggal", "Tanggal");
                sqlBulk.ColumnMappings.Add("Periode", "Periode");
                sqlBulk.ColumnMappings.Add("provinsi", "provinsi");
                sqlBulk.ColumnMappings.Add("jumlah_sid", "jumlah_sid");

                sqlBulk.WriteToServer(dataReader);
            }
            return View("Index", data);
        }
    }
}
