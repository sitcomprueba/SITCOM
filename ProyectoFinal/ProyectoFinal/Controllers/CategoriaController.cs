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
    public class CategoriaController : Controller
    {
        private SitcomEntities db = new SitcomEntities();

        // GET: /Categoria/
        public ActionResult Index()
        {
            var caracteristica = db.Caracteristica.Include(c => c.CategoriaCaracteristicas).Include(c => c.TipoCaracteristica);
            return View(caracteristica.ToList());
        }

        // GET: /Categoria/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caracteristica caracteristica = db.Caracteristica.Find(id);
            if (caracteristica == null)
            {
                return HttpNotFound();
            }
            return View(caracteristica);
        }

        // GET: /Categoria/Create
        public ActionResult Create()
        {
            ViewBag.idCategoriaCaracteristicas = new SelectList(db.CategoriaCaracteristicas, "idCategoriaCaracteristicas", "nombre");
            ViewBag.idTipoCaracteristica = new SelectList(db.TipoCaracteristica, "idTipoCaracteristica", "nombre");
            return View();
        }

        // POST: /Categoria/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="idCaracteristica,nombre,idCategoriaCaracteristicas,idTipoCaracteristica")] Caracteristica caracteristica)
        {
            if (ModelState.IsValid)
            {
                db.Caracteristica.Add(caracteristica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCategoriaCaracteristicas = new SelectList(db.CategoriaCaracteristicas, "idCategoriaCaracteristicas", "nombre", caracteristica.idCategoriaCaracteristicas);
            ViewBag.idTipoCaracteristica = new SelectList(db.TipoCaracteristica, "idTipoCaracteristica", "nombre", caracteristica.idTipoCaracteristica);
            return View(caracteristica);
        }

        // GET: /Categoria/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caracteristica caracteristica = db.Caracteristica.Find(id);
            if (caracteristica == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCategoriaCaracteristicas = new SelectList(db.CategoriaCaracteristicas, "idCategoriaCaracteristicas", "nombre", caracteristica.idCategoriaCaracteristicas);
            ViewBag.idTipoCaracteristica = new SelectList(db.TipoCaracteristica, "idTipoCaracteristica", "nombre", caracteristica.idTipoCaracteristica);
            return View(caracteristica);
        }

        // POST: /Categoria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="idCaracteristica,nombre,idCategoriaCaracteristicas,idTipoCaracteristica")] Caracteristica caracteristica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(caracteristica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCategoriaCaracteristicas = new SelectList(db.CategoriaCaracteristicas, "idCategoriaCaracteristicas", "nombre", caracteristica.idCategoriaCaracteristicas);
            ViewBag.idTipoCaracteristica = new SelectList(db.TipoCaracteristica, "idTipoCaracteristica", "nombre", caracteristica.idTipoCaracteristica);
            return View(caracteristica);
        }

        // GET: /Categoria/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caracteristica caracteristica = db.Caracteristica.Find(id);
            if (caracteristica == null)
            {
                return HttpNotFound();
            }
            return View(caracteristica);
        }

        // POST: /Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Caracteristica caracteristica = db.Caracteristica.Find(id);
            db.Caracteristica.Remove(caracteristica);
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
