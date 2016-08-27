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
    public class TipoHabitacionController : Controller
    {
        private SitcomEntities db = new SitcomEntities();

        // GET: /TipoHabitacion/
        public ActionResult Index()
        {
            return View(db.TipoHabitacion.ToList());
        }

        // GET: /TipoHabitacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoHabitacion tipohabitacion = db.TipoHabitacion.Find(id);
            if (tipohabitacion == null)
            {
                return HttpNotFound();
            }
            return View(tipohabitacion);
        }

        // GET: /TipoHabitacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TipoHabitacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="idTipoHabitacion,nombre")] TipoHabitacion tipohabitacion)
        {
            if (ModelState.IsValid)
            {
                db.TipoHabitacion.Add(tipohabitacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipohabitacion);
        }

        // GET: /TipoHabitacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoHabitacion tipohabitacion = db.TipoHabitacion.Find(id);
            if (tipohabitacion == null)
            {
                return HttpNotFound();
            }
            return View(tipohabitacion);
        }

        // POST: /TipoHabitacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="idTipoHabitacion,nombre")] TipoHabitacion tipohabitacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipohabitacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipohabitacion);
        }

        // GET: /TipoHabitacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoHabitacion tipohabitacion = db.TipoHabitacion.Find(id);
            if (tipohabitacion == null)
            {
                return HttpNotFound();
            }
            return View(tipohabitacion);
        }

        // POST: /TipoHabitacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoHabitacion tipohabitacion = db.TipoHabitacion.Find(id);
            db.TipoHabitacion.Remove(tipohabitacion);
            db.SaveChanges();
            return RedirectToAction("Index");
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
