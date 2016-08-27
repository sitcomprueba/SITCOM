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
    public class HabitacionesController : Controller
    {
        private SitcomEntities db = new SitcomEntities();

        // GET: /Habitaciones/
        public ActionResult Index(int idHotel)
        {
            var habitacion = db.Habitacion.Include(h => h.Hotel)
                                          .Include(h => h.TipoHabitacion)
                                          .Where(h => h.idHotel == idHotel);

            return View(habitacion.ToList());
        }

        // GET: /Habitaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Habitacion habitacion = db.Habitacion.Include(h => h.Hotel)
                                          .Include(h => h.TipoHabitacion)
                                          .Where(h => h.idHabitacion == id).FirstOrDefault();
            if (habitacion == null)
            {
                return HttpNotFound();
            }
            return View(habitacion);
        }

        // GET: /Habitaciones/Create
        public ActionResult Create(int? idHotel)
        {
            if (idHotel != null)
                ViewBag.idHotel = idHotel;
            ViewBag.idTipoHabitacion = new SelectList(db.TipoHabitacion, "idTipoHabitacion", "nombre");
            return View();
        }

        // POST: /Habitaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="idHabitacion,idTipoHabitacion,cantidadBanios,balcon,heladera,microondas,idHotel,nombreHabitacion")] Habitacion habitacion)
        {
            if (ModelState.IsValid)
            {
                db.Habitacion.Add(habitacion);
                db.SaveChanges();
                return RedirectToAction("Index", new { idHotel = habitacion.idHotel });
            }

            ViewBag.idHotel = habitacion.idHotel;
            ViewBag.idTipoHabitacion = new SelectList(db.TipoHabitacion, "idTipoHabitacion", "nombre", habitacion.idTipoHabitacion);
            return View(habitacion);
        }

        // GET: /Habitaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Habitacion habitacion = db.Habitacion.Find(id);
            if (habitacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.idTipoHabitacion = new SelectList(db.TipoHabitacion, "idTipoHabitacion", "nombre", habitacion.idTipoHabitacion);
            return View(habitacion);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="idHabitacion,idTipoHabitacion,cantidadBanios,balcon,heladera,microondas,nombreHabitacion,idHotel")] Habitacion habitacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(habitacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { idHotel = habitacion.idHotel });
            }
            ViewBag.idTipoHabitacion = new SelectList(db.TipoHabitacion, "idTipoHabitacion", "nombre", habitacion.idTipoHabitacion);
            return View(habitacion);
        }

        // GET: /Habitaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Habitacion habitacion = db.Habitacion.Include("TipoHabitacion").Where(h => h.idTipoHabitacion == id).FirstOrDefault();
            if (habitacion == null)
            {
                return HttpNotFound();
            }
            return View(habitacion);
        }

        // POST: /Habitaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Habitacion habitacion = db.Habitacion.Find(id);
            db.Habitacion.Remove(habitacion);
            db.SaveChanges();
            return RedirectToAction("Index", new { idHotel = habitacion.idHotel });
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
