using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using ORMFoy5.Models;

namespace ORMFoy5.Controllers
{
    public class OgrenciController : Controller
    {
        private readonly AppDbContext _context;

        public OgrenciController()
        {
            _context = new AppDbContext();
        }

        // Öğrenci listesi (Read)
        public ActionResult Index()
        {
            var ogrenciler = _context.Ogrenciler.Include(o => o.Bolum).ToList();
            return View(ogrenciler);
        }

        // Yeni öğrenci formu (Create - GET)
        public ActionResult Create()
        {
            ViewBag.BolumID = new SelectList(_context.Bolumler, "BolumID", "BolumAd");
            return View();
        }

        // Yeni öğrenci ekleme (Create - POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ogrenci ogrenci)
        {
            if (ModelState.IsValid)
            {
                _context.Ogrenciler.Add(ogrenci);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BolumID = new SelectList(_context.Bolumler, "BolumID", "BolumAd", ogrenci.BolumID);
            return View(ogrenci);
        }

        // Öğrenci düzenleme formu (Edit - GET)
        public ActionResult Edit(int id)
        {
            var ogrenci = _context.Ogrenciler.Find(id);
            if (ogrenci == null)
                return HttpNotFound();

            ViewBag.BolumID = new SelectList(_context.Bolumler, "BolumID", "BolumAd", ogrenci.BolumID);
            return View(ogrenci);
        }

        // Öğrenci güncelleme (Edit - POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ogrenci ogrenci)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(ogrenci).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BolumID = new SelectList(_context.Bolumler, "BolumID", "BolumAd", ogrenci.BolumID);
            return View(ogrenci);
        }

        // Silme onay sayfası (Delete - GET)
        public ActionResult Delete(int id)
        {
            var ogrenci = _context.Ogrenciler.Find(id);
            if (ogrenci == null)
                return HttpNotFound();

            return View(ogrenci);
        }

        // Silme işlemi (Delete - POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var ogrenci = _context.Ogrenciler.Find(id);
            _context.Ogrenciler.Remove(ogrenci);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // Öğrenci detayları (Details)
        public ActionResult Details(int id)
        {
            var ogrenci = _context.Ogrenciler.Include(o => o.Bolum).FirstOrDefault(o => o.OgrenciID == id);
            if (ogrenci == null)
                return HttpNotFound();

            return View(ogrenci);
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