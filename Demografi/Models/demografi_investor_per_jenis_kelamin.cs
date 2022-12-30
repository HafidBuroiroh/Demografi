using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demografi.Models
{
    public class demografi_investor_per_jenis_kelamin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int demografi_investor_per_jenis_kelamin_id { get; set; }
        public DateTime tanggal { get; set; }  
        public DateTime Periode { get; set; }
        public string? Jenis_kelamin { get; set; }
        public int jumlah_sid { get; set; }
    }
}
