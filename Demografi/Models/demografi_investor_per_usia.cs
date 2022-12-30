using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demografi.Models
{

    public class demografi_investor_per_usia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int demografi_investor_per_usia_id { get; set; }
        public DateTime tanggal { get; set; }
        public DateTime Periode { get; set; }
        public string? Usia { get; set; }
        public int jumlah_sid { get; set; }
    }
}
