using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace ProyectoFinal.Controllers
{
    public class DptoOCabanaController : Controller
    {
        private SitcomEntities db = new SitcomEntities();

        // GET: /CasaODpto/
        public ActionResult Index(int idComplejo)
        {
            var casadptoocabana = db.CasaDptoOCabana
                                           .Include(c => c.Complejo)
                                           .Include(c => c.LugarHospedaje)
                                           .Where(c => c.idComplejo == idComplejo);

            return View(casadptoocabana.ToList());
        }
        // GET: /CasaODpto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CasaDptoOCabana casadptoocabana = db.CasaDptoOCabana.Find(id);
            if (casadptoocabana == null)
            {
                return HttpNotFound();
            }
            return View(casadptoocabana);
        }
        // GET: /CasaODpto/Create
        public ActionResult Create(int idComplejo)
        {
            ViewBag.idComplejo = idComplejo;
            ViewBag.idLugarHospedaje = new SelectList(db.LugarHospedaje, "idLugarHospedaje", "idLugarHospedaje");
            return View();
        }
        // POST: /CasaODpto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="idCasaDptoOCabana,nombreCasaDptoOCabana,idLugarHospedaje,cantidadHabitaciones,cantidadAmbientes,cantidadBanios,idComplejo,capacidadMaxima")] CasaDptoOCabana casadptoocabana)
        {
            if (ModelState.IsValid)
            {
                db.CasaDptoOCabana.Add(casadptoocabana);
                db.SaveChanges();
                return RedirectToAction("Index", new { idComplejo = casadptoocabana.idComplejo });
            }

            ViewBag.idComplejo = new SelectList(db.Complejo, "idComplejo", "idComplejo", casadptoocabana.idComplejo);
            ViewBag.idLugarHospedaje = new SelectList(db.LugarHospedaje, "idLugarHospedaje", "idLugarHospedaje", casadptoocabana.idLugarHospedaje);
            return View(casadptoocabana);
        }
        // GET: /CasaODpto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CasaDptoOCabana casadptoocabana = db.CasaDptoOCabana.Find(id);
            if (casadptoocabana == null)
            {
                return HttpNotFound();
            }
            ViewBag.idComplejo = new SelectList(db.Complejo, "idComplejo", "idComplejo", casadptoocabana.idComplejo);
            ViewBag.idLugarHospedaje = new SelectList(db.LugarHospedaje, "idLugarHospedaje", "idLugarHospedaje", casadptoocabana.idLugarHospedaje);
            return View(casadptoocabana);
        }
        // POST: /CasaODpto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="idCasaDptoOCabana,nombreCasaDptoOCabana,idLugarHospedaje,cantidadHabitaciones,cantidadAmbientes,cantidadBanios,idComplejo,capacidadMaxima")] CasaDptoOCabana casadptoocabana)
        {
            if (ModelState.IsValid)
            {
                db.Entry(casadptoocabana).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { idComplejo = casadptoocabana.idComplejo });
            }
            ViewBag.idComplejo = new SelectList(db.Complejo, "idComplejo", "idComplejo", casadptoocabana.idComplejo);
            ViewBag.idLugarHospedaje = new SelectList(db.LugarHospedaje, "idLugarHospedaje", "idLugarHospedaje", casadptoocabana.idLugarHospedaje);
            return View(casadptoocabana);
        }
        // GET: /CasaODpto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CasaDptoOCabana casadptoocabana = db.CasaDptoOCabana.Find(id);
            if (casadptoocabana == null)
            {
                return HttpNotFound();
            }
            return View(casadptoocabana);
        }
        // POST: /CasaODpto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CasaDptoOCabana casadptoocabana = db.CasaDptoOCabana.Find(id);
            int idComplejo = (int)casadptoocabana.idComplejo;
            db.CasaDptoOCabana.Remove(casadptoocabana);
            db.SaveChanges();
            return RedirectToAction("Index", new { idComplejo = idComplejo });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
