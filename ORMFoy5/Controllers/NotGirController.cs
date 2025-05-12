using System;
using System.Linq;
using System.Web.Mvc;
using ORMFoy5.Models; // Models namespace'ini doğru tanımladık

namespace ORMFoy5.Controllers
{
    public class NotGirController : Controller
    {
        private readonly AppDbContext _context = new AppDbContext(); // AppDbContext tanımlı olmalı

        public ActionResult Index(int? dersID)
        {
            var dersler = _context.Dersler.ToList();
            ViewBag.Dersler = new SelectList(dersler, "DersID", "DersAd", dersID);

            if (!dersID.HasValue)
            {
                return View(new OgrenciDers[0]);
            }

            var ogrenciDersler = _context.OgrenciDersler
                .Include("Ogrenci")
                .Include("Ders")
                .Where(od => od.DersID == dersID)
                .ToList();

            return View(ogrenciDersler);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KaydetNotlar(int[] ogrenciDersID, int[] vizeNotu, int[] finalNotu, int dersID)
        {
            for (int i = 0; i < ogrenciDersID.Length; i++)
            {
                var ogrenciDers = _context.OgrenciDersler.Find(ogrenciDersID[i]);
                if (ogrenciDers != null)
                {
                    ogrenciDers.VizeNotu = vizeNotu[i];
                    ogrenciDers.FinalNotu = finalNotu[i];
                }
            }
            _context.SaveChanges();

            var ogrenciDersler = _context.OgrenciDersler
                .Include("Ogrenci")
                .Include("Ders")
                .Where(od => od.DersID == dersID)
                .ToList();

            return View("NotlarKaydedildi", ogrenciDersler);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}