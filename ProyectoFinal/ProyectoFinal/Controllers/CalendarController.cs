using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BussinesEntities;
using BL;

namespace ProyectoFinal.Controllers
{
    public class CalendarController : Controller
    {

        private CalendarManager _calendar = new CalendarManager();
        private HotelManager hm = new HotelManager();

        //
        // GET: /Calendar/
        public ActionResult Index()
        {
            //List<DisponibilidadEntity> lista= new List<DisponibilidadEntity>();
            //DisponibilidadEntity objDispo = new DisponibilidadEntity();
            var model = _calendar.getCalender(DateTime.Now.Month, DateTime.Now.Year);
            //lista = hm.consultarDisponibilidad(1);


            //foreach (var dispo in lista)
            //{
              

            //    foreach (var diaSemana1 in model.Week1)
            //    {

            //        if (diaSemana1 != null)
            //        {

            //            if (diaSemana1.Date == dispo.fechaDisponible && dispo.estaOcupado == 1)
            //            {
            //                diaSemana1.estaOcupado = true;
            //            }
                    
                    
            //        }

                   

                    
            //    }


            //    foreach (var diaSemana2 in model.Week2)
            //    {

            //        if (diaSemana2 != null)
            //        {

            //            if (diaSemana2.Date == dispo.fechaDisponible && dispo.estaOcupado == 1)
            //            {
            //                diaSemana2.estaOcupado = true;
            //            }


            //        }



            //    }

            //    foreach (var diaSemana3 in model.Week3)
            //    {
            //        if (diaSemana3 != null)
            //        {

            //            if (diaSemana3.Date == dispo.fechaDisponible && dispo.estaOcupado == 1)
            //            {
            //                diaSemana3.estaOcupado = true;
            //            }


            //        }

            //    }

            //    foreach (var diaSemana4 in model.Week4)
            //    {
            //        if (diaSemana4 != null)
            //        {

            //            if (diaSemana4.Date == dispo.fechaDisponible && dispo.estaOcupado == 1)
            //            {
            //                diaSemana4.estaOcupado = true;
            //            }


            //        }


            //    }

            //    foreach (var diaSemana5 in model.Week5)
            //    {
            //        if (diaSemana5 != null)
            //        {

            //            if (diaSemana5.Date == dispo.fechaDisponible && dispo.estaOcupado == 1)
            //            {
            //                diaSemana5.estaOcupado = true;
            //            }


            //        }


            //    }

            //    foreach (var diaSemana6 in model.Week6)
            //    {
            //        if (diaSemana6 != null)
            //        {

            //            if (diaSemana6.Date == dispo.fechaDisponible && dispo.estaOcupado == 1)
            //            {
            //                diaSemana6.estaOcupado = true;
            //            }


            //        }


            //    }

                
            //}

            


         
            return View(model);

        }

        //for ajax request
        public ActionResult AsyncUpdateCalender(int month, int year)
        {
            if (HttpContext.Request.IsAjaxRequest())
            {
                var model = _calendar.getCalender(month, year);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View();
            }
        }
	}
}