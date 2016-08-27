using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal.Controllers
{
    public class ErroresController : Controller
    {
        //
        // GET: /Errores/
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ErrorPermisos()
        {
            return View();
        }
	}
}