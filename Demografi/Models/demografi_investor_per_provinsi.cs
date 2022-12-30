using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demografi.Models
{
    public class demografi_investor_per_provinsi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int demografi_investor_per_provinsi_id { get; set; }
        public DateTime Tanggal { get; set;  }
        public DateTime Periode { get; set; }
        public string? provinsi { get; set; }
        public int jumlah_sid { get; set; }
    }
}
