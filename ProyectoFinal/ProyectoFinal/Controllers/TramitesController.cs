using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL;
using BussinesEntities;
using DAL;

namespace ProyectoFinal.Controllers
{
    public class TramitesController : Controller
    {
        //
        // GET: /Tramites/
        private UsuarioEntity usuarioActual;
        private UsuariosManager um = new UsuariosManager();
        private TramitesManager tm = new TramitesManager();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TramitesResp(int? id)
        {
            List<TramiteEntity> tramites = tm.GetAllTramites(id);

            foreach (var item in tramites)
            {
                if (item.idUsuarioResponsable == null)
                    item.Usuarios1 = new Usuarios() { nombreUsuario = "No Asignado" };
            }
            return View(tramites);
        }
        public ActionResult TomarTramiteResp(int id)
        {
            ObtenerUsuarioActual();
            TramiteEntity tra = tm.GetTramiteById(id);
            tm.CambiarEstadoTramite(tra, 2, usuarioActual);

            return RedirectToAction("TramitesResp");
        }
        public ActionResult CancelarTramite(int id, string returnUrl)
        {
            ObtenerUsuarioActual();
            TramiteEntity tra = tm.GetTramiteById(id);
            tm.CambiarEstadoTramite(tra, 5, usuarioActual);
            return RedirectToAction(returnUrl, new { mensaje = "¡Tu tramite fue cancelado!" });
        }
        public ActionResult TramitesUsuario(string mensaje)
        {
            ObtenerUsuarioActual();

            ViewBag.Perfil = usuarioActual.idPerfil;
            ViewBag.UsuarioActual = usuarioActual;
            ViewBag.Mensaje = mensaje;
            List<TramiteEntity> tramites = tm.GetTramitesByUsuario(usuarioActual);

            return View(tramites);
        }
        public ActionResult VerTramite(int id)
        {
            TramiteEntity tra = tm.GetTramiteById(id);

            switch (tra.idTipoTramite)
            {
                case 1: return RedirectToAction("EvalNegocio", "Negocios", new { id = tra.idNegocio, idTramite = tra.idTramite});
                case 2: return RedirectToAction("EvalSolicitudModulo", "Reservas", new { id = tra.idNegocio, idTramite = tra.idTramite });
                case 3: return RedirectToAction("EvalNegocio", "Negocios", new { id = tra.idNegocio, idTramite = tra.idTramite });
                default: return RedirectToAction("Index","Home");
           
            }
        }
        public ActionResult ResolverTramite(int idtramite, int accion, string comentario)
        {
            ObtenerUsuarioActual();
            TramiteEntity tra = tm.GetTramiteById(idtramite);
            tm.CambiarEstadoTramite(tra, accion, usuarioActual);

            return RedirectToAction("PanelControlUsuario", "Usuarios", new { mensaje = "Se resolvio el tramite correctamente!" });
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