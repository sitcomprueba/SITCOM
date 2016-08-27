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
    public class CategoriaHospedajeController : Controller
    {
        private SitcomEntities db = new SitcomEntities();

        // GET: /CategoriaHospedaje/
        public ActionResult Index()
        {
            return View(db.CategoriaHospedaje.ToList());
        }

        // GET: /CategoriaHospedaje/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaHospedaje categoriahospedaje = db.CategoriaHospedaje.Find(id);
            if (categoriahospedaje == null)
            {
                return HttpNotFound();
            }
            return View(categoriahospedaje);
        }

        // GET: /CategoriaHospedaje/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /CategoriaHospedaje/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="idCategoria,nombre")] CategoriaHospedaje categoriahospedaje)
        {
            if (ModelState.IsValid)
            {
                db.CategoriaHospedaje.Add(categoriahospedaje);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoriahospedaje);
        }

        // GET: /CategoriaHospedaje/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaHospedaje categoriahospedaje = db.CategoriaHospedaje.Find(id);
            if (categoriahospedaje == null)
            {
                return HttpNotFound();
            }
            return View(categoriahospedaje);
        }

        // POST: /CategoriaHospedaje/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="idCategoria,nombre")] CategoriaHospedaje categoriahospedaje)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoriahospedaje).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoriahospedaje);
        }

        // GET: /CategoriaHospedaje/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaHospedaje categoriahospedaje = db.CategoriaHospedaje.Find(id);
            if (categoriahospedaje == null)
            {
                return HttpNotFound();
            }
            return View(categoriahospedaje);
        }

        // POST: /CategoriaHospedaje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoriaHospedaje categoriahospedaje = db.CategoriaHospedaje.Find(id);
            db.CategoriaHospedaje.Remove(categoriahospedaje);
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
