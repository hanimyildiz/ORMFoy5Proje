using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ORMFoy5.Models;

namespace ORMFoy5.Controllers
{
    public class OgrenciDersController : Controller
    {
        private readonly AppDbContext _context = new AppDbContext();

        public ActionResult Index()
        {
            var ogrenciDersler = _context.OgrenciDersler
                .Include(od => od.Ogrenci)
                .Include(od => od.Ders)
                .ToList();

            return View(ogrenciDersler);
        }

        public ActionResult Create()
        {
            ViewBag.OgrenciID = new SelectList(_context.Ogrenciler, "OgrenciID", "Ad");
            ViewBag.DersID = new SelectList(_context.Dersler, "DersID", "DersAd");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OgrenciDers ogrenciDers)
        {
            if (ModelState.IsValid)
            {
                _context.OgrenciDersler.Add(ogrenciDers);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OgrenciID = new SelectList(_context.Ogrenciler, "OgrenciID", "Ad", ogrenciDers.OgrenciID);
            ViewBag.DersID = new SelectList(_context.Dersler, "DersID", "DersAd", ogrenciDers.DersID);
            return View(ogrenciDers);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var ogrenciDers = _context.OgrenciDersler.Find(id);
            if (ogrenciDers == null) return HttpNotFound();

            ViewBag.OgrenciID = new SelectList(_context.Ogrenciler, "OgrenciID", "Ad", ogrenciDers.OgrenciID);
            ViewBag.DersID = new SelectList(_context.Dersler, "DersID", "DersAd", ogrenciDers.DersID);
            return View(ogrenciDers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OgrenciDers ogrenciDers)
        {
            if (ModelState.IsValid)
            {
                var existing = _context.OgrenciDersler.Find(ogrenciDers.OgrenciDersID);
                if (existing == null) return HttpNotFound();

                existing.OgrenciID = ogrenciDers.OgrenciID;
                existing.DersID = ogrenciDers.DersID;
                existing.Yil = ogrenciDers.Yil;
                existing.Yariyil = ogrenciDers.Yariyil;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OgrenciID = new SelectList(_context.Ogrenciler, "OgrenciID", "Ad", ogrenciDers.OgrenciID);
            ViewBag.DersID = new SelectList(_context.Dersler, "DersID", "DersAd", ogrenciDers.DersID);
            return View(ogrenciDers);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var ogrenciDers = _context.OgrenciDersler
                .Include(od => od.Ogrenci)
                .Include(od => od.Ders)
                .FirstOrDefault(od => od.OgrenciDersID == id);

            if (ogrenciDers == null) return HttpNotFound();

            return View(ogrenciDers);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var ogrenciDers = _context.OgrenciDersler.Find(id);
            if (ogrenciDers == null) return HttpNotFound();

            _context.OgrenciDersler.Remove(ogrenciDers);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(400);
            var ogrenciDers = _context.OgrenciDersler.Find(id);
            if (ogrenciDers == null) return HttpNotFound();
            return View(ogrenciDers);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
