using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORMFoy5.Models
{
    public class Bolum
    {
        [Key]
        public int BolumID { get; set; }

        [Required]
        public string BolumAd { get; set; }

        [ForeignKey("Fakulte")]
        public int FakulteID { get; set; }

        public virtual Fakulte Fakulte { get; set; }

        public virtual ICollection<Ogrenci> Ogrenciler { get; set; }

        public virtual ICollection<Ders> Dersler { get; set; } // << yeni
    }
}
