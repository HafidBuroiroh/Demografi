using DocumentFormat.OpenXml.Office2013.PowerPoint.Roaming;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demografi.Models
{
    public class demografi_investor_email_parameter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int demografi_investor_email_parameter_id { get; set; }
        public string? email { get; set; }
    }
}
