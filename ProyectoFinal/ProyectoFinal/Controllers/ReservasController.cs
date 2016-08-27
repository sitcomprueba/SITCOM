using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL;
using DAL;
using BussinesEntities;

namespace ProyectoFinal.Controllers
{
    public class ReservasController : Controller
    {
        UsuarioEntity usuarioActual;
        SitcomEntities db = new SitcomEntities();
        NegociosManager nm = new NegociosManager();
        UsuariosManager um = new UsuariosManager();
        PersonasManager pm = new PersonasManager();
        ReservasManager rm = new ReservasManager();

        // GET: /Reservas/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SolicitarModulo(int? idNegocio)
        {
            ObtenerUsuarioActual();
            NegocioEntity neg = nm.GetNegocioById((int)idNegocio);
            Persona p = pm.GetPersonaById((int)usuarioActual.idPersona);

            ViewBag.Persona = p;

            return View(neg);
        }
        [HttpPost]
        public ActionResult SolicitarModulo(bool aceptaCondiciones, int idNegocio)
        {
            ObtenerUsuarioActual();
            if(aceptaCondiciones == true)
            {
                rm.SolicitarModuloReservas(idNegocio, usuarioActual);
                return RedirectToAction("Index","Home");
            }
            else
            {                
                NegocioEntity neg = nm.GetNegocioById(idNegocio);

                Persona p = pm.GetPersonaById(usuarioActual.idUsuario);
                ViewBag.Persona = p;

                ViewBag.Error = 1;
                return View("SolicitarModulo",neg);
            }
        }
        public ActionResult EvalSolicitudModulo(int id, int idTramite)
        {
            NegocioEntity neg = nm.GetNegocioById(id);
            Persona p = pm.GetPersonaById((int)neg.Usuarios.idPersona);

            ViewBag.Persona = p;
            ViewBag.Tramite = idTramite;

            return View(neg);
        }
        public ActionResult GestionReservas(int idNegocio)
        {
            NegocioEntity neg = nm.GetNegocioById(idNegocio);

            return View(neg);
        }
        public ActionResult SolicitarReserva(int id)
        {
            ObtenerUsuarioActual();
            if(usuarioActual.idPersona != null)
            {
                NegocioEntity neg = nm.GetNegocioById(id);
                Persona p = pm.GetPersonaById((int)usuarioActual.idPersona);

                ViewBag.idTipoLugarHospedaje = neg.LugarHospedaje.FirstOrDefault().idTipoLugarHospedaje;
                ViewBag.idNegocio = neg.idNegocio;
                ViewBag.Persona = p;

                return View();
            }
            else
            {
                return RedirectToAction("DatosPersonales", "Persona", new { returnUrl = "../Reservas/SolicitarReserva/" + id });
            }
        }
        [HttpPost]
        public ActionResult SolicitarReserva(SolicitudEntity solicitud, string fechaDesde, string fechaHasta, int idNegocio)
        {
            ObtenerUsuarioActual();
            solicitud.fechaDesde = Convert.ToDateTime(fechaDesde);
            solicitud.fechaHasta = Convert.ToDateTime(fechaHasta);
            solicitud.idNegocio = idNegocio;
            solicitud.idUsuarioSolicitante = usuarioActual.idUsuario;

            rm.AddSolicitudReserva(solicitud);

            return RedirectToAction("IndexHospedajes", "Negocios");
        }
        public ActionResult SolicitudesReserva(int idNegocio)
        {
            List<SolicitudEntity> solicitudes = rm.GetSolicitudesReserva(idNegocio);
            ViewBag.idNegocio = idNegocio;

            return View(solicitudes);
        }
        public ActionResult VerSolicitudReserva(int idSolicitud)
        {
            SolicitudEntity sol = rm.GetSolicitudReservaById(idSolicitud);
            Persona p = pm.GetPersonaById((int)sol.Usuarios.idPersona);
            NegocioEntity neg = nm.GetNegocioById((int)sol.idNegocio);

            ViewBag.Persona = p;
            ViewBag.idTipoLugarHospedaje = neg.LugarHospedaje.FirstOrDefault().idTipoLugarHospedaje;

            return View(sol);
        }
        public ActionResult TusHabitaciones(int idLugarHospedaje)
        {
            int idHotel = int.Parse(db.Hotel.Where(h => h.idLugarHospedaje == idLugarHospedaje).Select(h => h.idHotel).FirstOrDefault().ToString());
            return RedirectToAction("Index", "Habitaciones", new { idHotel = idHotel });
        }
        public ActionResult TusDptosOCabanas(int idLugarHospedaje)
        {
            int idComplejo = int.Parse(db.Complejo.Where(c => c.idLugarHospedaje == idLugarHospedaje).Select(c => c.idComplejo).FirstOrDefault().ToString());
            return RedirectToAction("Index", "DptoOCabana", new { idComplejo = idComplejo });
        }
        public bool ValidarPermisoVista(string controlador, string vista) //METODO ÚNICO DEL CONTROLADOR PARA VALIDAR PERMISO DE LA VISTA (LLAMA AL MANEJADOR).
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