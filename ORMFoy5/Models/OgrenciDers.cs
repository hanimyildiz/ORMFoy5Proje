using System;

namespace ORMFoy5.Models
{
    public class OgrenciDers
    {
        public int OgrenciDersID { get; set; }
        public int OgrenciID { get; set; }
        public int DersID { get; set; }

        public int Yil { get; set; }

        public string Yariyil { get; set; } // DÜZENLENDİ: int → string

        public int? VizeNotu { get; set; }  // nullable yapıldı
        public int? FinalNotu { get; set; } // nullable yapıldı

        public virtual Ogrenci Ogrenci { get; set; }
        public virtual Ders Ders { get; set; }

        public OgrenciDers()
        {
            Yil = DateTime.Now.Year;
            Yariyil = "Güz"; // Varsayılan yarıyıl örneği
        }
    }
}
