using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BussinesEntities;
using BL;
using DAL;

namespace ProyectoFinal.Controllers
{
    public class DomicilioController : Controller
    {
        DomicilioEntity domicilioEN = new DomicilioEntity();
        DomicilioManager dm = new DomicilioManager();
        private SitcomEntities db = new SitcomEntities();
        private UsuarioEntity usuarioActual;
        //
        // GET: /Domicilio/C:\Proyecto Final\branches\branch-1.1-BackEnd-1Iteracion\Mati\ProyectoFinal\ProyectoFinal\ProyectoFinal\Content\
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PruebaDomicilio()
        {
            domicilioEN.listPaises = dm.GetAllPaises();
            ViewBag.ReturnURl = "PruebaDomicilio";

            return View(domicilioEN);
        }
        public ActionResult PaisSeleccionado(int? idPaisSeleccionado, string returnUrl)
        {
            domicilioEN.listPaises = dm.GetAllPaises();
            if(idPaisSeleccionado != null)
            {
                domicilioEN.paisSeleccionado = (int)idPaisSeleccionado;
                domicilioEN.listProvincias = new List<Provincia>();

                if (idPaisSeleccionado.HasValue)
                    domicilioEN.listProvincias = dm.getProvinciaPaisSeleccionado(idPaisSeleccionado);
            }

            ViewBag.ReturnURl = returnUrl;
            
            return View(returnUrl, domicilioEN);
        }
        public ActionResult ProvinciaSeleccionada(int? idProvinciaSeleccionada, int idPaisSeleccionado, string returnUrl)
        {
            domicilioEN.listPaises = dm.GetAllPaises();
            domicilioEN.paisSeleccionado = idPaisSeleccionado;
            if (idProvinciaSeleccionada != null)
            {
                domicilioEN.listLocalidades = new List<Localidad>();

                if (idProvinciaSeleccionada.HasValue)
                {
                    domicilioEN.listLocalidades = dm.getLocalidadByProvinciaSeleccionada(idProvinciaSeleccionada);
                }

                
                domicilioEN.listProvincias = dm.getProvinciaPaisSeleccionado(idPaisSeleccionado);
                domicilioEN.provinciaSeleccionada = (int)idProvinciaSeleccionada;               
            }

            ViewBag.ReturnURl = returnUrl;

            return View(returnUrl, domicilioEN);
        }


        // COMERCIO //
        public ActionResult PaisSeleccionadoComercio(NegocioEntity negocio,
                                                     [Bind(Include = "idRubro")] ComercioEntity comercio,
                                                     [Bind(Include = "paisSeleccionado,provinciaSeleccionada,localidadSeleccionada,barrio,calle,dpto,numero")] DomicilioEntity domEn)
        {
            ObtenerUsuarioActual();
            domicilioEN.listPaises = dm.GetAllPaises();
            negocio.Comercio.Add(comercio);
            negocio.idTipoNegocio = 2;

            if (domEn.paisSeleccionado != null)
            {
                domicilioEN.paisSeleccionado = domEn.paisSeleccionado;

                domicilioEN.listProvincias = dm.getProvinciaPaisSeleccionado(domEn.paisSeleccionado);
            }

            negocio.Sucursal.Add(new SucursalEntity()
            {
                 Domicilio = domicilioEN
            });

            ViewBag.Perfil = usuarioActual.idPerfil;
            ViewBag.Rubros = new SelectList(db.Rubro, "idRubro", "nombreRubro", negocio.Comercio.FirstOrDefault().idRubro);
            ViewBag.TiposNegocio = new SelectList(db.TipoDeNegocio, "idTipoNegocio", "nombre", negocio.idTipoNegocio);

            return View("Negocios/Nuevo", negocio);
        }

        public void ObtenerUsuarioActual()
        {
            usuarioActual = (UsuarioEntity)Session["User"];
        }

	}
}