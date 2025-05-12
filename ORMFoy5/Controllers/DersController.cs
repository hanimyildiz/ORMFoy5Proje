using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using ORMFoy5.Models;
using System.Net;

namespace ORMFoy5.Controllers
{
    public class DersController : Controller
    {
        private readonly AppDbContext _context;

        public DersController()
        {
            _context = new AppDbContext();
        }

        // Index (Listele)
        public ActionResult Index()
        {
            var dersler = _context.Dersler.ToList();
            return View(dersler);
        }

        // Create - GET
        public ActionResult Create()
        {
            ViewBag.BolumID = new SelectList(_context.Bolumler, "BolumID", "BolumAd");
            return View();
        }

        // Create - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ders ders)
        {
            if (ModelState.IsValid)
            {
                _context.Dersler.Add(ders);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BolumID = new SelectList(_context.Bolumler, "BolumID", "BolumAd", ders.BolumID);
            return View(ders);
        }

        // Edit - GET
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ders = _context.Dersler.Find(id);
            if (ders == null)
            {
                return HttpNotFound();
            }
            ViewBag.BolumID = new SelectList(_context.Bolumler, "BolumID", "BolumAd", ders.BolumID);
            return View(ders);
        }

        // Edit - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ders ders)
        {
            if (ModelState.IsValid)
            {
                var existingDers = _context.Dersler.Find(ders.DersID);
                if (existingDers == null)
                {
                    return HttpNotFound();
                }

                existingDers.DersAd = ders.DersAd;
                existingDers.BolumID = ders.BolumID;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BolumID = new SelectList(_context.Bolumler, "BolumID", "BolumAd", ders.BolumID);
            return View(ders);
        }

        // Details - GET
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ders = _context.Dersler.Find(id);
            if (ders == null)
            {
                return HttpNotFound();
            }
            return View(ders);
        }

        // Delete - GET
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ders = _context.Dersler.Find(id);
            if (ders == null)
            {
                return HttpNotFound();
            }
            return View(ders);
        }

        // Delete - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var ders = _context.Dersler.Find(id);
            if (ders == null)
            {
                return HttpNotFound();
            }
            _context.Dersler.Remove(ders);
            _context.SaveChanges();
            return RedirectToAction("Index");
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