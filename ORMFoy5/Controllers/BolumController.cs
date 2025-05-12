using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using ORMFoy5.Models;
using System.Net;

namespace ORMFoy5.Controllers
{
    public class BolumController : Controller
    {
        private readonly AppDbContext _context;

        public BolumController()
        {
            _context = new AppDbContext();
        }

        // Index (Listele)
        public ActionResult Index()
        {
            var bolumler = _context.Bolumler.ToList();
            return View(bolumler);
        }

        // Create - GET
        public ActionResult Create()
        {
            ViewBag.FakulteID = new SelectList(_context.Fakulteler, "FakulteID", "FakulteAd");
            return View();
        }

        // Create - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Bolum bolum)
        {
            if (ModelState.IsValid)
            {
                _context.Bolumler.Add(bolum);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FakulteID = new SelectList(_context.Fakulteler, "FakulteID", "FakulteAd", bolum.FakulteID);
            return View(bolum);
        }

        // Edit - GET
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bolum = _context.Bolumler.Find(id);
            if (bolum == null)
            {
                return HttpNotFound();
            }
            ViewBag.FakulteID = new SelectList(_context.Fakulteler, "FakulteID", "FakulteAd", bolum.FakulteID);
            return View(bolum);
        }

        // Edit - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Bolum bolum)
        {
            if (ModelState.IsValid)
            {
                var existingBolum = _context.Bolumler.Find(bolum.BolumID);
                if (existingBolum == null)
                {
                    return HttpNotFound();
                }

                existingBolum.BolumAd = bolum.BolumAd;
                existingBolum.FakulteID = bolum.FakulteID;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FakulteID = new SelectList(_context.Fakulteler, "FakulteID", "FakulteAd", bolum.FakulteID);
            return View(bolum);
        }

        // Details - GET
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bolum = _context.Bolumler.Find(id);
            if (bolum == null)
            {
                return HttpNotFound();
            }
            return View(bolum);
        }

        // Delete - GET
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bolum = _context.Bolumler.Find(id);
            if (bolum == null)
            {
                return HttpNotFound();
            }
            return View(bolum);
        }

        // Delete - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var bolum = _context.Bolumler.Find(id);
            if (bolum == null)
            {
                return HttpNotFound();
            }
            _context.Bolumler.Remove(bolum);
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