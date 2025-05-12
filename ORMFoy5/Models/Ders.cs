using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORMFoy5.Models
{
    public class Ders
    {
        [Key]
        public int DersID { get; set; }

        [Required]
        public string DersAd { get; set; }

        [ForeignKey("Bolum")]
        public int BolumID { get; set; }

        public virtual Bolum Bolum { get; set; }

        public virtual ICollection<OgrenciDers> OgrenciDersleri { get; set; }
    }
}
