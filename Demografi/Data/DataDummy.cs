using Demografi.Models;
using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;


namespace Demografi.Data
{
    public class DataDummy
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();
            if (context.demografi_investor_general.Any())
            {
                return;   // DB has been seeded
            }

            var dummygeneral = new demografi_investor_general[]
            {
                new demografi_investor_general{ tanggal =DateTime.Parse("2022-09-01"), Periode =DateTime.Parse("2022-09-01"), jumlah_sre = 190, jumlah_sid = 200, jumlah_sre_aktif_terhubung_sid = 100, jumlah_sre_aktif_tidak_terhubung_sid = 90, jumlah_sid_dengan_sre_aktif = 170, jumlah_sid_dengan_sre_closed = 20, jumlah_sre_aktif_local = 70, jumlah_sre_aktif_asing = 130, jumlah_sid_local = 100, jumlah_sid_asing = 150},
                new demografi_investor_general{tanggal =DateTime.Parse("2022-09-02"), Periode =DateTime.Parse("2022-09-02"), jumlah_sre = 170, jumlah_sid = 290, jumlah_sre_aktif_terhubung_sid = 120, jumlah_sre_aktif_tidak_terhubung_sid = 50, jumlah_sid_dengan_sre_aktif = 190, jumlah_sid_dengan_sre_closed = 20, jumlah_sre_aktif_local = 170, jumlah_sre_aktif_asing = 130, jumlah_sid_local = 100, jumlah_sid_asing = 110},
                new demografi_investor_general{tanggal =DateTime.Parse("2022-09-03"), Periode =DateTime.Parse("2022-09-03"), jumlah_sre = 130, jumlah_sid = 170, jumlah_sre_aktif_terhubung_sid = 100, jumlah_sre_aktif_tidak_terhubung_sid = 30, jumlah_sid_dengan_sre_aktif = 90, jumlah_sid_dengan_sre_closed = 40, jumlah_sre_aktif_local = 90, jumlah_sre_aktif_asing = 100, jumlah_sid_local = 180, jumlah_sid_asing = 170},
                new demografi_investor_general{tanggal =DateTime.Parse("2022-09-04"), Periode =DateTime.Parse("2022-09-04"), jumlah_sre = 100, jumlah_sid = 300, jumlah_sre_aktif_terhubung_sid = 70, jumlah_sre_aktif_tidak_terhubung_sid = 230, jumlah_sid_dengan_sre_aktif = 70, jumlah_sid_dengan_sre_closed = 30, jumlah_sre_aktif_local = 170, jumlah_sre_aktif_asing = 130, jumlah_sid_local = 28, jumlah_sid_asing = 20},
            };
            foreach (demografi_investor_general s in dummygeneral)
            {
                context.demografi_investor_general.Add(s);
            }
            context.SaveChanges();

            var dummyjk = new demografi_investor_per_jenis_kelamin[]
            {
                new demografi_investor_per_jenis_kelamin{tanggal = DateTime.Parse("2022-09-01"), Periode =DateTime.Parse("2022-09-02"), Jenis_kelamin = "MALE", jumlah_sid=90},
                new demografi_investor_per_jenis_kelamin{tanggal = DateTime.Parse("2022-09-02"), Periode =DateTime.Parse("2022-09-02"), Jenis_kelamin = "FEMALE", jumlah_sid=60},
            };
            foreach (demografi_investor_per_jenis_kelamin s in dummyjk)
            {
                context.demografi_investor_per_jenis_kelamin.Add(s);
            }
            context.SaveChanges();

            var dummyprov = new demografi_investor_per_provinsi[]
            {
                new demografi_investor_per_provinsi{Tanggal = DateTime.Parse("2022-09-01"), Periode =DateTime.Parse("2022-09-01"), provinsi = "BALI", jumlah_sid = 30},
                new demografi_investor_per_provinsi{Tanggal = DateTime.Parse("2022-09-02"), Periode =DateTime.Parse("2022-09-02"), provinsi = "KALIMANTAN TIMUR", jumlah_sid = 40},
                new demografi_investor_per_provinsi{Tanggal = DateTime.Parse("2022-09-03"), Periode =DateTime.Parse("2022-09-03"), provinsi = "JAWA BARAT", jumlah_sid = 50},
                new demografi_investor_per_provinsi{Tanggal = DateTime.Parse("2022-09-04"), Periode =DateTime.Parse("2022-09-04"), provinsi = "JAKARTA SELATAN", jumlah_sid = 60},
                new demografi_investor_per_provinsi{Tanggal = DateTime.Parse("2022-09-05"), Periode =DateTime.Parse("2022-09-05"), provinsi = "JAKARTA PUSAT", jumlah_sid = 90}
            };
            foreach (demografi_investor_per_provinsi s in dummyprov)
            {
                context.demografi_investor_per_provinsi.Add(s);
            }
            context.SaveChanges();

            var dummyefek = new demografi_investor_per_tipe_efek[]
            {
                new demografi_investor_per_tipe_efek{tanggal = DateTime.Parse("2022-09-01"), Periode =DateTime.Parse("2022-09-01"), Tipe_efek = "EQUITY", jumlah_sid = 90, jumlah_sre=100},
                new demografi_investor_per_tipe_efek{tanggal = DateTime.Parse("2022-09-02"), Periode =DateTime.Parse("2022-09-02"), Tipe_efek = "GOVERMENT BOND", jumlah_sid = 70, jumlah_sre=90},
                new demografi_investor_per_tipe_efek{tanggal = DateTime.Parse("2022-09-03"), Periode =DateTime.Parse("2022-09-03"), Tipe_efek = "CORPORATE BOND", jumlah_sid = 100, jumlah_sre=80},
                new demografi_investor_per_tipe_efek{tanggal = DateTime.Parse("2022-09-04"), Periode =DateTime.Parse("2022-09-04"), Tipe_efek = "WARRANT", jumlah_sid = 40, jumlah_sre=190},
                new demografi_investor_per_tipe_efek{tanggal = DateTime.Parse("2022-09-05"), Periode =DateTime.Parse("2022-09-05"), Tipe_efek = "EBA", jumlah_sid = 50, jumlah_sre=900},
            };
            foreach (demografi_investor_per_tipe_efek s in dummyefek)
            {
                context.demografi_investor_per_tipe_efek.Add(s);
            }
            context.SaveChanges();

            var dummyusia = new demografi_investor_per_usia[]
            {
                new demografi_investor_per_usia{tanggal = DateTime.Parse("2022-09-01"), Periode =DateTime.Parse("2022-09-01"), 
                    Usia = "16-20", jumlah_sid = 100},
                new demografi_investor_per_usia{tanggal = DateTime.Parse("2022-09-02"), Periode =DateTime.Parse("2022-09-02"), Usia = "51-55", jumlah_sid = 190},
                new demografi_investor_per_usia{tanggal = DateTime.Parse("2022-09-03"), Periode =DateTime.Parse("2022-09-03"), Usia = "36-40", jumlah_sid = 220},
                new demografi_investor_per_usia{tanggal = DateTime.Parse("2022-09-04"), Periode =DateTime.Parse("2022-09-04"), Usia = "20-25", jumlah_sid = 170},
            };
            foreach (demografi_investor_per_usia s in dummyusia)
            {
                context.demografi_investor_per_usia.Add(s);
            }
            context.SaveChanges();
        }
    }
}
