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
    public class TransferdataController : Controller
    {
        private DateTime? DateFrom;
        private DateTime? DateUntil;
        private readonly DataContext _context;
        public IConfiguration _Configuration { get; set; }

        public TransferdataController(IConfiguration configuration, DataContext context)
        {
            _Configuration = configuration;
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.demografi_investor_general.ToList());
        }

        public IActionResult Transfer(DateTime from, DateTime until, string btnsubmit)
        {
            var data = from d in _context.demografi_investor_general select d;
            ViewData["CurrentFIlter"] = from;
            ViewData["CurrentFilters"] = until;

            if (btnsubmit == "Search")
            {
                if (from != null && until != null)
                {
                    data = data.Where(d => d.tanggal >= from && d.tanggal <= until);
                }
            }
            if (btnsubmit == "Transfer")
            {
                var connectionstring = _Configuration["ConnectionStrings:DefaultConnection"];
                SqlConnection con = new SqlConnection(connectionstring);
                var sql = $"SELECT * FROM demografi_investor_general WHERE tanggal Between '{from.ToString("yyyy-MM-dd")}' AND '{until.ToString("yyyy-MM-dd")}'";
                SqlCommand com = new SqlCommand(sql, con);
                con.Open();

                SqlDataReader dataReader = com.ExecuteReader();

                SqlBulkCopy sqlBulk = new SqlBulkCopy(con);
                sqlBulk.DestinationTableName = "dbo.demografi_investor_general_saved";

                sqlBulk.ColumnMappings.Add("demografi_investor_general_id", "demografi_investor_general_id");
                sqlBulk.ColumnMappings.Add("tanggal", "tanggal");
                sqlBulk.ColumnMappings.Add("Periode", "Periode");
                sqlBulk.ColumnMappings.Add("jumlah_sre", "jumlah_sre");
                sqlBulk.ColumnMappings.Add("jumlah_sid", "jumlah_sid");
                sqlBulk.ColumnMappings.Add("jumlah_sre_aktif_terhubung_sid", "jumlah_sre_aktif_terhubung_sid");
                sqlBulk.ColumnMappings.Add("jumlah_sre_aktif_tidak_terhubung_sid", "jumlah_sre_aktif_tidak_terhubung_sid");
                sqlBulk.ColumnMappings.Add("jumlah_sid_dengan_sre_aktif", "jumlah_sid_dengan_sre_aktif");
                sqlBulk.ColumnMappings.Add("jumlah_sid_dengan_sre_closed", "jumlah_sid_dengan_sre_closed");
                sqlBulk.ColumnMappings.Add("jumlah_sre_aktif_local", "jumlah_sre_aktif_local");
                sqlBulk.ColumnMappings.Add("jumlah_sre_aktif_asing", "jumlah_sre_aktif_asing");
                sqlBulk.ColumnMappings.Add("jumlah_sid_local", "jumlah_sid_local");
                sqlBulk.ColumnMappings.Add("jumlah_sid_asing", "jumlah_sid_asing");

                sqlBulk.WriteToServer(dataReader);
            }
            return View("Index", data);
        }
    }
}
