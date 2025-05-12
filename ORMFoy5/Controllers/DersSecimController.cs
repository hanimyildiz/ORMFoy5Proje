using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ORMFoy5.Models;

namespace ORMFoy5.Controllers
{
    public class DersSecim
    {
        public string DersAdi { get; set; }
        public int? SecenOgrenciSayisi { get; set; }
        public int? Yil { get; set; }
        public string Yariyil { get; set; }
    }

    public class DersSecimController : Controller
    {
        private readonly AppDbContext _context = new AppDbContext();

        public ActionResult Index(int? ogrenciID, int? yil, string yariyil)
        {
            List<DersSecim> result = new List<DersSecim>();

            if (ogrenciID.HasValue)
            {
                result = _context.OgrenciDersler
                    .Where(od => od.OgrenciID == ogrenciID)
                    .Select(od => new DersSecim
                    {
                        DersAdi = od.Ders.DersAd,
                        Yil = od.Yil,
                        Yariyil = od.Yariyil
                    })
                    .ToList();
            }
            else if (yil.HasValue && !string.IsNullOrEmpty(yariyil))
            {
                result = _context.OgrenciDersler
                    .Where(od => od.Yil == yil && od.Yariyil.ToLower() == yariyil.ToLower())
                    .GroupBy(od => od.Ders.DersAd)
                    .Select(g => new DersSecim
                    {
                        DersAdi = g.Key,
                        SecenOgrenciSayisi = g.Count()
                    })
                    .ToList();
            }

            return View(result);
        }
    }
}
