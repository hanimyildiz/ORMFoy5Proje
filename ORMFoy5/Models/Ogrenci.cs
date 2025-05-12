using System.Collections.Generic;

namespace ORMFoy5.Models
{
    public class Ogrenci
    {
        public int OgrenciID { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public int BolumID { get; set; }
        public virtual Bolum Bolum { get; set; }
        public virtual ICollection<OgrenciDers> OgrenciDersleri { get; set; } // Öğrencinin aldığı dersler

        public Ogrenci()
        {
            OgrenciDersleri = new List<OgrenciDers>();
        }
    }
}