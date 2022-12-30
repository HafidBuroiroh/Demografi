using DocumentFormat.OpenXml.Office2013.PowerPoint.Roaming;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demografi.Models
{
    public class demografi_investor_general
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int demografi_investor_general_id { get; set; }
        public DateTime tanggal { get; set; }
        public DateTime Periode { get; set; }
        public int jumlah_sre { get; set; }
        public int jumlah_sid { get; set; }
        public int jumlah_sre_aktif_terhubung_sid { get; set; }
        public int jumlah_sre_aktif_tidak_terhubung_sid { get; set; }
        public int jumlah_sid_dengan_sre_aktif { get; set;  }
        public int jumlah_sid_dengan_sre_closed { get; set; }
        public int jumlah_sre_aktif_local { get; set; }
        public int jumlah_sre_aktif_asing { get; set; }
        public int jumlah_sid_local { get; set; }
        public int jumlah_sid_asing { get; set; }
    }
}
