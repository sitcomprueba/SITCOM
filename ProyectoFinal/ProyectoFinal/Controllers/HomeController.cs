using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using BL;
using BussinesEntities;

namespace ProyectoFinal.Controllers
{
    public class HomeController : Controller
    {
        public UsuarioEntity usuarioActual;
        public UsuariosManager um = new UsuariosManager();
        public ActionResult Index()
        {
            ObtenerUsuarioActual();

            if (usuarioActual.idUsuario == 0)
                usuarioActual.idPerfil = 0;

            ViewBag.Perfil = usuarioActual.idPerfil;
            return View(usuarioActual);
        }

        public ActionResult Hospedajes()
        {
            ViewBag.Message = "Consultá los mejores lugares de hospedaje!";

            return View();
        }

        public ActionResult AltaNegocio()
        {
            return View();
        }

        public ActionResult Comercios()
        {
            ViewBag.Message = "Consultá los comercios que podes visitar en tu estadía!";

            return View();
        }

        public ActionResult GestionUsuarios()
        {
            return RedirectToAction("Index", "Usuarios");
        }

        public bool ValidarPermisoVista(string controlador, string vista) //METODO UNICO DEL CONTROLADOR PARA VALIDAR PERMISO DE LA VISTA (LLAMA AL MANEJADOR).
        {
            ObtenerUsuarioActual();
            return um.ValidarPermisoVista(usuarioActual, controlador, vista);
        }
        public void ObtenerUsuarioActual()
        {
            usuarioActual = (UsuarioEntity)Session["User"];
        }
    }
}