using Demografi.Models;
using Microsoft.EntityFrameworkCore;

namespace Demografi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<demografi_investor_general> demografi_investor_general { get; set; }
        public DbSet<demografi_investor_general_saved> demografi_investor_general_saved { get; set; }
        public DbSet<demografi_investor_per_jenis_kelamin> demografi_investor_per_jenis_kelamin { get; set; }
        public DbSet<demografi_investor_per_provinsi> demografi_investor_per_provinsi { get; set; }
        public DbSet<demografi_investor_per_provinsi_saved> demografi_investor_per_provinsi_saved { get; set; }
        public DbSet<demografi_investor_per_jenis_kelamin_saved> demografi_investor_per_jenis_kelamin_saved { get; set; }
        public DbSet<demografi_investor_per_usia> demografi_investor_per_usia { get; set; }
        public DbSet<demografi_investor_per_usia_saved> demografi_investor_per_usia_saved { get; set; }
        public DbSet<demografi_investor_per_tipe_efek> demografi_investor_per_tipe_efek { get; set; }
        public DbSet<demografi_investor_per_tipe_efek_saved> demografi_investor_per_tipe_efek_saved { get; set; }
        public DbSet<demografi_investor_email_parameter> demografi_investor_email_parameter { get; set; }

    }
}
