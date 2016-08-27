using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BussinesEntities;
using System.Text;
using BL;
using DAL;
using System.Globalization;


namespace ProyectoFinal.Controllers
{
    public class ComplejoController : Controller
    {
        UsuarioEntity usuarioActual = new UsuarioEntity();
        UsuariosManager um = new UsuariosManager();
        SitcomEntities db = new SitcomEntities();

        List<DisponibilidadEntity> listDispo = new List<DisponibilidadEntity>();
        ConjuntoDisponibildiadEntity conju = new ConjuntoDisponibildiadEntity();
        NegociosManager nm = new NegociosManager();
        ComplejoManager cm = new ComplejoManager();
        private static int idComplejoActual;


        private CalendarManager _calendar = new CalendarManager();
     




        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Disponibilidad(int? id)
        {
            NegocioEntity neg = nm.GetNegocioById((int)id);
            idComplejoActual = neg.LugarHospedaje.FirstOrDefault().Complejo.FirstOrDefault().idComplejo;


            conju.ListaDisponibilidad = listDispo;

            List<CasaDptoOCabana> listCasa = cm.getCasaDptoById(idComplejoActual);
            ViewBag.Casas = listCasa;


            return View(conju);

        }

        [HttpPost]
        public ActionResult Disponibilidad(ConjuntoDisponibildiadEntity conj)
        {

            int i = 1;
            String indice = "FechaDesde_" + i;

            ConjuntoDisponibildiadEntity conju = new ConjuntoDisponibildiadEntity();
            List<DisponibilidadEntity> lstDisponibilidad = new List<DisponibilidadEntity>();


            while (Request.Form[indice] != null)
            {
                DisponibilidadEntity objDisp = new DisponibilidadEntity();



                int idCasa = int.Parse(Request.Form["idCasa_" + i]);
                String fechaDesde = Request.Form["fechaDesde_" + i];
                String fechaHasta = Request.Form["fechaHasta_" + i];


                objDisp.idCasaODpto = idCasa;
                objDisp.fechaDesde = Convert.ToDateTime(fechaDesde);
                objDisp.fechaHasta = Convert.ToDateTime(fechaHasta);


                lstDisponibilidad.Add(objDisp);

                i++;

                indice = "FechaDesde_" + i;


            }


            foreach (var item in lstDisponibilidad)
            {

                item.habilitado = true;
                item.idEstado = 1;
                cm.registrarDisponibilidad(item);

            }


            List<CasaDptoOCabana> listCasa = cm.getCasaDptoById(idComplejoActual);
            ViewBag.Casas = listCasa;

            return View(conju);

        }
        public ActionResult ConsultarDisponibilidad(string fechaDesde, string fechaHasta, int cantidadPersonas, int cantidadHabitaciones, int idNegocio)
        {

            NegocioEntity neg = nm.GetNegocioById(idNegocio);
            int idComplejo = neg.LugarHospedaje.FirstOrDefault().Complejo.FirstOrDefault().idComplejo;

            DateTime fechaDes = Convert.ToDateTime(fechaDesde);
            DateTime fechaHas = Convert.ToDateTime(fechaHasta);

            List<DisponibilidadEntity> lista = new List<DisponibilidadEntity>();
            DisponibilidadEntity objDispo = new DisponibilidadEntity();
            var model = _calendar.getCalender(fechaDes.Month, fechaDes.Year);

            lista = cm.consultarDisponibilidad(idComplejo, cantidadHabitaciones, cantidadPersonas, fechaDes.Year, fechaDes.Month);


            foreach (var dispo in lista)
            {


                foreach (var diaSemana1 in model.Week1)
                {

                    if (diaSemana1 != null)
                    {

                        if (diaSemana1.Date == dispo.fechaDisponible && dispo.estaOcupado == 1)
                        {
                            diaSemana1.estaOcupado = true;
                        }


                    }




                }


                foreach (var diaSemana2 in model.Week2)
                {

                    if (diaSemana2 != null)
                    {

                        if (diaSemana2.Date == dispo.fechaDisponible && dispo.estaOcupado == 1)
                        {
                            diaSemana2.estaOcupado = true;
                        }


                    }



                }

                foreach (var diaSemana3 in model.Week3)
                {
                    if (diaSemana3 != null)
                    {

                        if (diaSemana3.Date == dispo.fechaDisponible && dispo.estaOcupado == 1)
                        {
                            diaSemana3.estaOcupado = true;
                        }


                    }

                }

                foreach (var diaSemana4 in model.Week4)
                {
                    if (diaSemana4 != null)
                    {

                        if (diaSemana4.Date == dispo.fechaDisponible && dispo.estaOcupado == 1)
                        {
                            diaSemana4.estaOcupado = true;
                        }


                    }


                }

                foreach (var diaSemana5 in model.Week5)
                {
                    if (diaSemana5 != null)
                    {

                        if (diaSemana5.Date == dispo.fechaDisponible && dispo.estaOcupado == 1)
                        {
                            diaSemana5.estaOcupado = true;
                        }


                    }


                }

                foreach (var diaSemana6 in model.Week6)
                {
                    if (diaSemana6 != null)
                    {

                        if (diaSemana6.Date == dispo.fechaDisponible && dispo.estaOcupado == 1)
                        {
                            diaSemana6.estaOcupado = true;
                        }


                    }


                }


            }





            ObtenerUsuarioActual();
            var tieneTramiteMGR = db.Tramite.Where(t => t.idNegocio == neg.idNegocio && (t.idTipoTramite == 2 && t.idEstadoTramite == 1 || t.idEstadoTramite == 2)).FirstOrDefault();

            var tieneMGR = neg.LugarHospedaje.FirstOrDefault().moduloReservas;

            ViewBag.TieneMGR = tieneMGR;

            if (tieneTramiteMGR != null)
                ViewBag.TieneTramiteMGR = 1;
            else
                ViewBag.TieneTramiteMGR = 0;

            if (usuarioActual.idUsuario == neg.idUsuario)
                ViewBag.EsDueno = 1;
            else
                ViewBag.EsDueno = 0;

            ViewBag.ModelCalendar = model;

            return View("../Negocios/VerHospedaje", neg);

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