using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL;
using BussinesEntities;
using DAL;
using System.IO;

namespace ProyectoFinal.Controllers
{
    public class NegociosController : Controller
    {
        private SitcomEntities db = new SitcomEntities();
        private UsuarioEntity usuarioActual;
        private NegociosManager nm = new NegociosManager();
        private UsuariosManager um = new UsuariosManager();
        private DomicilioManager dm = new DomicilioManager();
        private NegocioEntity neg = new NegocioEntity();
        private DomicilioEntity dom = new DomicilioEntity();
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Nuevo() //PANTALLA CREAR NUEVO - NEGOCIOS
        {
            ObtenerUsuarioActual();
            if(usuarioActual.idPersona != null)
            {
                /*if (ValidarPermisoVista("Negocios", "Nuevo"))
                {*/
                ViewBag.Perfil = usuarioActual.idPerfil;
                ViewBag.TiposNegocio = new SelectList(db.TipoDeNegocio, "idTipoNegocio", "nombre");
                ViewBag.Rubros = new SelectList(db.Rubro, "idRubro", "nombreRubro");


                NegocioEntity neg = new NegocioEntity();//para tener un idNegocio=0 y no falle al llamar a la accion NuevoComercio.

                dom.listLocalidadesCercanas = dm.GetLocalidadesCercanas();
                SucursalEntity suc = new SucursalEntity()
                {
                    Domicilio = dom
                };
                neg.Sucursal.Add(suc);

                return View(neg);
            }
            else
            {
                return RedirectToAction("DatosPersonales", "Persona", new { returnUrl = "../Negocios/Nuevo" });
            }
            /*}
            else
                return RedirectToAction("ErrorPermisos", "Errores");*/
        }
        public ActionResult NuevoComercio(NegocioEntity negocio,
                                          [Bind(Include = "idRubro" )] ComercioEntity comercio,
                                          [Bind(Include = "localidadSeleccionada,barrio,calle,dpto,numero")] DomicilioEntity domEn,
                                          string telefono,
                                          HttpPostedFileBase imagenPrinc,
                                          HttpPostedFileBase imagenMuestra1,
                                          HttpPostedFileBase imagenMuestra2,
                                          HttpPostedFileBase imagenMuestra3,
                                          HttpPostedFileBase imagenMuestra4,
                                          HttpPostedFileBase imagenMuestra5,
                                          HttpPostedFileBase imagenMuestra6)
        {
            ObtenerUsuarioActual();

            neg.idUsuario = usuarioActual.idUsuario;
            neg.idTipoNegocio = 2;
            neg.nombre = negocio.nombre;
            neg.descripcion = negocio.descripcion;
            neg.Comercio = new List<ComercioEntity>() { comercio };

            neg.Sucursal.Add(new SucursalEntity()
            {
                esPrincipal = true,
                telefono = telefono,
                Domicilio = domEn
            });

            byte[] buffer = null;
            using (var binaryReader = new BinaryReader(imagenPrinc.InputStream))
            {
                buffer = binaryReader.ReadBytes(imagenPrinc.ContentLength);
            }

            FotosNegocio fotoPrinc = new FotosNegocio()
            {
                foto = buffer,
                esPrincipal = true
            };

            List<HttpPostedFileBase> listaHttp = new List<HttpPostedFileBase>() { imagenMuestra1, imagenMuestra2, imagenMuestra3, imagenMuestra4, imagenMuestra5, imagenMuestra6 };

            List<FotosNegocio> fotosNegocio = convertirImagenesMuestra(listaHttp);
            fotosNegocio.Add(fotoPrinc);

            neg.FotosNegocio = fotosNegocio;

            if (ModelState.IsValid)
            {
                if (nm.ValidarExisteNegocio(neg.nombre,null))
                    nm.AddNegocio(neg,usuarioActual);
                else
                {
                    ModelState.AddModelError("", "Un comercio con el mismo nombre ya está registrado. Por favor elige otro.");
                    ViewBag.Perfil = usuarioActual.idPerfil;
                    ViewBag.Rubros = new SelectList(db.Rubro, "idRubro", "nombreRubro", neg.Comercio.FirstOrDefault().idRubro);
                    ViewBag.TiposNegocio = new SelectList(db.TipoDeNegocio, "idTipoNegocio", "nombre", neg.idTipoNegocio);
                    return View("Nuevo", neg);
                }
            }

            return RedirectToAction("Index", "Home");
        }
        public ActionResult NuevoLugarHospedaje([Bind(Include = "nombre,descripcion")] NegocioEntity negocio,
                                                [Bind(Include = "localidadSeleccionada,barrio,calle,dpto,numero")] DomicilioEntity domEn,
                                           HttpPostedFileBase imagenPrinc,
                                           HttpPostedFileBase imagenMuestra1,
                                           HttpPostedFileBase imagenMuestra2,
                                           HttpPostedFileBase imagenMuestra3,
                                           HttpPostedFileBase imagenMuestra4,
                                           HttpPostedFileBase imagenMuestra5,
                                           HttpPostedFileBase imagenMuestra6,
                                           string telefono,
                                           int TipoHospedaje)
        {
            // PARTE NEGOCIO //
            ObtenerUsuarioActual();

            neg.idUsuario = usuarioActual.idUsuario;
            neg.idTipoNegocio = 1;
            neg.nombre = negocio.nombre;
            neg.descripcion = negocio.descripcion;
            neg.Sucursal.Add(new SucursalEntity()
            {
                esPrincipal = true,
                telefono = telefono,
                Domicilio = domEn
            });

            // FIN PARTE NEGOCIO //

            // PARTE IMAGENES //
            byte[] buffer = null;
            using (var binaryReader = new BinaryReader(imagenPrinc.InputStream))
            {
                buffer = binaryReader.ReadBytes(imagenPrinc.ContentLength);
            }

            FotosNegocio fotoPrinc = new FotosNegocio()
            {
                foto = buffer,
                esPrincipal = true
            };

            List<HttpPostedFileBase> listaHttp = new List<HttpPostedFileBase>() { imagenMuestra1, imagenMuestra2, imagenMuestra3, imagenMuestra4, imagenMuestra5, imagenMuestra6 };

            List<FotosNegocio> fotosNegocio = convertirImagenesMuestra(listaHttp);
            fotosNegocio.Add(fotoPrinc);

            neg.FotosNegocio = fotosNegocio;

            // FIN PARTE IMAGENES //

            // PARTE LUGAR HOSPEDAJE //
            LugarHospedajeEntity lug = new LugarHospedajeEntity();
            lug.idTipoLugarHospedaje = TipoHospedaje;
            lug.CaracteristicasHospedaje = new List<CaracteristicasHospedajeEntity>();
            neg.LugarHospedaje = new List<LugarHospedajeEntity>() { lug };

            SelectList carac = new SelectList(db.Caracteristica, "idCaracteristica", "nombre");
            foreach (var item in carac)
            {
                string s = Request.Form[item.Text];
                bool selected = false;

                if (s != "false" && s != null)
                    selected = true;

                if (selected)
                {
                    CaracteristicasHospedajeEntity c = new CaracteristicasHospedajeEntity()
                    {
                       idCaracteristica = int.Parse(item.Value)                            
                    };
                    neg.LugarHospedaje.FirstOrDefault().CaracteristicasHospedaje.Add(c);
                }
            }
                      

            switch (neg.LugarHospedaje.FirstOrDefault().idTipoLugarHospedaje)
            {
                case 1: List<CasaDptoOCabana> casaDptosOCabanas = new List<CasaDptoOCabana>() { new CasaDptoOCabana() };
                        neg.LugarHospedaje.FirstOrDefault().CasaDptoOCabana = casaDptosOCabanas;
                    break;

                case 2: List<ComplejoEntity> complejos = new List<ComplejoEntity>() { new ComplejoEntity() };
                        neg.LugarHospedaje.FirstOrDefault().Complejo = complejos;
                        neg.LugarHospedaje.FirstOrDefault().Complejo.FirstOrDefault().idCategoria = int.Parse(Request.Form["categ"].ToString());
                        neg.LugarHospedaje.FirstOrDefault().Complejo.FirstOrDefault().idTipoComplejo = int.Parse(Request.Form["tipos"].ToString());

                        int j = 0;
                        int capacidad;
                        int cantidad;
                        string idCapacidad = "Capacidad_" + j;
                        string idCantidad = "Cantidad_" + j;
                        int k = 1; //Para los nombres de las Casas/Dptos/Cabañas

                        List<CasaDptoOCabana> casasDptosOCabanas = new List<CasaDptoOCabana>();
                    
                        while(Request.Form[idCapacidad] != null && Request.Form[idCantidad] != null)
                        {
                            capacidad = int.Parse(Request.Form[idCapacidad]);
                            cantidad = int.Parse(Request.Form[idCantidad]);

                            for (int i = 0; i < cantidad; i++)
                            {
                                casasDptosOCabanas.Add(new CasaDptoOCabana()
                                    {
                                        nombreCasaDptoOCabana = "Alojamiento " + k,
                                        capacidadMaxima = capacidad
                                    });
                                k++;
                            }
                            j++;
                            idCapacidad = "Capacidad_" + j;
                            idCantidad = "Cantidad_" + j;
                        }

                        neg.LugarHospedaje.FirstOrDefault().Complejo.FirstOrDefault().CasaDptoOCabana = casasDptosOCabanas;
                        
                    break;
                case 3: List<HotelEntity> hoteles = new List<HotelEntity>() { new HotelEntity() };
                        neg.LugarHospedaje.FirstOrDefault().Hotel = hoteles;
                        neg.LugarHospedaje.FirstOrDefault().Hotel.FirstOrDefault().idCategoria = int.Parse(Request.Form["categ"].ToString());

                        SelectList tiposHabitacion = new SelectList(db.TipoHabitacion, "idTipoHabitacion", "nombre");
                        List<Habitacion> habitaciones = new List<Habitacion>();
                        int m = 1; //Para los nombres de las habitaciones

                         foreach (var item in tiposHabitacion)
                         {
                             if(Request.Form[item.Text] != "")
                             {
                                 int num = int.Parse(Request.Form[item.Text]);
                                 for (int i = 0; i < num; i++)
                                 {
                                habitaciones.Add(new Habitacion()
                                {
                                    nombreHabitacion = "Habitación " + m,
                                    idTipoHabitacion = int.Parse(item.Value)
                                });
                                m++;
                                 }
                             }
                         }

                         neg.LugarHospedaje.FirstOrDefault().Hotel.FirstOrDefault().Habitacion = habitaciones;
                        break;

                default:
                    break;
            }

            // FIN PARTE LUGAR HOSPEDAJE //

            // PARTE INSERCIÓN NEGOCIO //
            if (ModelState.IsValid)
            {
                if (nm.ValidarExisteNegocio(neg.nombre,null))
                    nm.AddNegocio(neg, usuarioActual);
                else
                {
                    ModelState.AddModelError("", "Un comercio con el mismo nombre ya está registrado. Por favor elige otro.");

                    ViewBag.Perfil = usuarioActual.idPerfil;
                    ViewBag.Rubros = new SelectList(db.Rubro, "idRubro", "nombreRubro");
                    ViewBag.TiposNegocio = new SelectList(db.TipoDeNegocio, "idTipoNegocio", "nombre", neg.idTipoNegocio);
                    ViewBag.CaracHotel = new SelectList(db.Caracteristica, "idCaracteristica", "nombre");
                    ViewBag.Categorias = new SelectList(db.CategoriaHospedaje, "idCategoria", "nombre");

                    return View("Nuevo", neg);
                }
            }

            return RedirectToAction("Index", "Home");
        }
        public ActionResult IndexComercios()
        {
            var result = nm.GetAllNegocios(2);
            return View(result);
        }
        public ActionResult IndexHospedajes()
        {
                var result = nm.GetAllNegocios(1);
                return View(result);
        }
        public ActionResult ObtenerImagen(int id)
        {
            var img = nm.GetFotoNegocioById(id);
            if(img != null)
            {
                byte[] buffer = img.foto;
                return File(buffer, "image/jpg", string.Format("{0}.jpg", id));
            }
            return null;
        }
        public ActionResult VerNegocio(int id)
        {
            NegocioEntity neg = nm.GetNegocioById(id);

            return View(neg);
        }
        public ActionResult EvalNegocio(int? id, int idTramite)
        {
            if (id != null)
            {
                NegocioEntity neg = nm.GetNegocioById((int)id);
                ViewBag.Tramite = idTramite;

                return View(neg);
            }
            else
                return RedirectToAction("Index", "Home");
        }
        public ActionResult NegociosUsuario()
        {
            ObtenerUsuarioActual();
            List<Negocio> negs = nm.GetNegocioByUsuario(usuarioActual.idUsuario);

            ViewBag.Perfil = usuarioActual.idPerfil;
            ViewBag.UsuarioActual = usuarioActual;

            return View(negs);
        }
        public ActionResult VerHospedaje(int? id)
        {
            ObtenerUsuarioActual();
            NegocioEntity neg = nm.GetNegocioById((int)id);

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

            return View(neg);
        }
        public ActionResult VerComercio(int? id)
        {
            NegocioEntity neg = nm.GetNegocioById((int)id);
            
            return View(neg);
        }
        public ActionResult NuevoCom(int? idNegocio)
        {
            ViewBag.Rubros = new SelectList(db.Rubro, "idRubro", "nombreRubro");

            NegocioEntity neg = new NegocioEntity();

            if(idNegocio != 0)
                 neg = nm.GetNegocioById((int)idNegocio);

            return View(neg);
        } //NO LA USO - POR SI NO FUNCIONA LA VISTA PARCIAL//
        public ActionResult NuevoCasaODpto()
        {
            dom.listLocalidadesCercanas = dm.GetLocalidadesCercanas();
            SucursalEntity suc = new SucursalEntity()
            {
                Domicilio = dom
            };
            neg.Sucursal.Add(suc);
            ViewBag.Carac = nm.GetCaracteristicas();
            ViewBag.Categorias = new SelectList(db.CategoriaHospedaje, "idCategoria", "nombre");
            return View(neg);
        }
        public ActionResult EditCasaODpto(int? idNegocio)
        {
            NegocioEntity negocio = nm.GetNegocioById((int)idNegocio);
            negocio.Sucursal.FirstOrDefault().Domicilio.listLocalidadesCercanas = dm.GetLocalidadesCercanas();

            ViewBag.Carac = nm.GetCaracteristicas();
            ViewBag.Categorias = new SelectList(db.CategoriaHospedaje, "idCategoria", "nombre");
            ViewBag.IdNegocio = negocio.idNegocio;
            return View(negocio);
        }
        public ActionResult NuevoHotel()
        {
            dom.listLocalidadesCercanas = dm.GetLocalidadesCercanas();
            SucursalEntity suc = new SucursalEntity()
            {
                Domicilio = dom
            };
            neg.Sucursal.Add(suc);
            ViewBag.Carac = nm.GetCaracteristicas();
            ViewBag.Categorias = new SelectList(db.CategoriaHospedaje, "idCategoria", "nombre");

            List<TipoHabitacion> habs = nm.GetTiposHabitacion();
            List<HabitacionesEntity> habitaciones = new List<HabitacionesEntity>();
            foreach (var item in habs)
            {
                habitaciones.Add(new HabitacionesEntity()
                {
                    idTipoHabitacion = item.idTipoHabitacion,
                    nombre = item.nombre
                });
            }
            ViewBag.Habitaciones = habitaciones;

            return View(neg);
        }
        public ActionResult EditHotel(int? idNegocio)
        {
            NegocioEntity negocio = nm.GetNegocioById((int)idNegocio);
            negocio.Sucursal.FirstOrDefault().Domicilio.listLocalidadesCercanas = dm.GetLocalidadesCercanas();

            ViewBag.Carac = nm.GetCaracteristicas();
            ViewBag.Categorias = new SelectList(db.CategoriaHospedaje, "idCategoria", "nombre");
            ViewBag.IdNegocio = idNegocio;

            return View(negocio);
        }
        public ActionResult NuevoComplejo()
        {
            dom.listLocalidadesCercanas = dm.GetLocalidadesCercanas();
            SucursalEntity suc = new SucursalEntity()
            {
                Domicilio = dom
            };
            neg.Sucursal.Add(suc);
            ViewBag.Carac = nm.GetCaracteristicas();
            ViewBag.Categorias = new SelectList(db.CategoriaHospedaje, "idCategoria", "nombre");
            ViewBag.TiposComplejo = new SelectList(db.TipoComplejo, "idTipoComplejo", "nombreTipoComplejo");
            return View(neg);
        }
        public ActionResult EditComplejo(int? idNegocio)
        {
            NegocioEntity negocio = nm.GetNegocioById((int)idNegocio);
            negocio.Sucursal.FirstOrDefault().Domicilio.listLocalidadesCercanas = dm.GetLocalidadesCercanas();
            
            ViewBag.Carac = nm.GetCaracteristicas();
            ViewBag.Categorias = new SelectList(db.CategoriaHospedaje, "idCategoria", "nombre");
            ViewBag.TiposComplejo = new SelectList(db.TipoComplejo, "idTipoComplejo", "nombreTipoComplejo");
            ViewBag.IdNegocio = negocio.idNegocio;

            return View(negocio);
        }
        public ActionResult EditHospedaje(int? idNegocio)
        {
            ObtenerUsuarioActual();
            NegocioEntity negocio = nm.GetNegocioById((int)idNegocio);

            switch (negocio.LugarHospedaje.FirstOrDefault().idTipoLugarHospedaje)
            {
                case 1: return RedirectToAction("EditCasaODpto", new { idNegocio = negocio.idNegocio });
                        break;
                case 2: return RedirectToAction("EditComplejo", new { idNegocio = negocio.idNegocio });
                        break;
                case 3: return RedirectToAction("EditHotel", new { idNegocio = negocio.idNegocio });
                        break;
                default: break;
            }

            return View(neg);
        }
        [HttpPost]
        public ActionResult EditHospedaje([Bind(Include = "nombre,descripcion")] NegocioEntity negocio,
                                          [Bind(Include = "localidadSeleccionada,barrio,calle,dpto,numero")] DomicilioEntity domEn,
                                          HttpPostedFileBase imagenPrinc,
                                          HttpPostedFileBase imagenMuestra1,
                                          HttpPostedFileBase imagenMuestra2,
                                          HttpPostedFileBase imagenMuestra3,
                                          HttpPostedFileBase imagenMuestra4,
                                          HttpPostedFileBase imagenMuestra5,
                                          HttpPostedFileBase imagenMuestra6,
                                          string telefono,
                                          int TipoHospedaje,
                                          int idNegocioOrig)
        {
            // PARTE NEGOCIO //
            ObtenerUsuarioActual();

            neg.idNegocioModif = idNegocioOrig; //Id del negocio que se modifica
            neg.idUsuario = usuarioActual.idUsuario;
            neg.idTipoNegocio = 1;
            neg.nombre = negocio.nombre;
            neg.descripcion = negocio.descripcion;
            neg.Sucursal.Add(new SucursalEntity()
            {
                esPrincipal = true,
                telefono = telefono,
                Domicilio = domEn
            });

            // FIN PARTE NEGOCIO //

            // PARTE IMAGENES //
            byte[] buffer = null;
            using (var binaryReader = new BinaryReader(imagenPrinc.InputStream))
            {
                buffer = binaryReader.ReadBytes(imagenPrinc.ContentLength);
            }

            FotosNegocio fotoPrinc = new FotosNegocio()
            {
                foto = buffer,
                esPrincipal = true
            };

            List<HttpPostedFileBase> listaHttp = new List<HttpPostedFileBase>() { imagenMuestra1, imagenMuestra2, imagenMuestra3, imagenMuestra4, imagenMuestra5, imagenMuestra6 };

            List<FotosNegocio> fotosNegocio = convertirImagenesMuestra(listaHttp);
            fotosNegocio.Add(fotoPrinc);

            neg.FotosNegocio = fotosNegocio;

            // FIN PARTE IMAGENES //

            // PARTE LUGAR HOSPEDAJE //
            LugarHospedajeEntity lug = new LugarHospedajeEntity();
            lug.idTipoLugarHospedaje = TipoHospedaje;
            lug.CaracteristicasHospedaje = new List<CaracteristicasHospedajeEntity>();
            neg.LugarHospedaje = new List<LugarHospedajeEntity>() { lug };

            SelectList carac = new SelectList(db.Caracteristica, "idCaracteristica", "nombre");
            foreach (var item in carac)
            {
                string s = Request.Form[item.Text];
                bool selected = false;

                if (s != "false" && s != null)
                    selected = true;

                if (selected)
                {
                    CaracteristicasHospedajeEntity c = new CaracteristicasHospedajeEntity()
                    {
                        idCaracteristica = int.Parse(item.Value)
                    };
                    neg.LugarHospedaje.FirstOrDefault().CaracteristicasHospedaje.Add(c);
                }
            }


            switch (neg.LugarHospedaje.FirstOrDefault().idTipoLugarHospedaje)
            {
                case 1: List<CasaDptoOCabana> casaDptosOCabanas = new List<CasaDptoOCabana>() { new CasaDptoOCabana() };
                    neg.LugarHospedaje.FirstOrDefault().CasaDptoOCabana = casaDptosOCabanas;
                    break;

                case 2: List<ComplejoEntity> complejos = new List<ComplejoEntity>() { new ComplejoEntity() };
                    neg.LugarHospedaje.FirstOrDefault().Complejo = complejos;
                    neg.LugarHospedaje.FirstOrDefault().Complejo.FirstOrDefault().idCategoria = int.Parse(Request.Form["categ"].ToString());
                    neg.LugarHospedaje.FirstOrDefault().Complejo.FirstOrDefault().idTipoComplejo = int.Parse(Request.Form["tipos"].ToString());
                                  

                    break;
                case 3: List<HotelEntity> hoteles = new List<HotelEntity>() { new HotelEntity() };
                    neg.LugarHospedaje.FirstOrDefault().Hotel = hoteles;
                    neg.LugarHospedaje.FirstOrDefault().Hotel.FirstOrDefault().idCategoria = int.Parse(Request.Form["categ"].ToString());
                                        
                    break;

                default:
                    break;
            }

            // FIN PARTE LUGAR HOSPEDAJE //

            // PARTE INSERCIÓN NEGOCIO //
            if (ModelState.IsValid)
            {
                if (nm.ValidarExisteNegocio(neg.nombre,neg.idNegocioModif))
                    nm.AddNegocio(neg, usuarioActual);
                else
                {
                    ModelState.AddModelError("", "Un comercio con el mismo nombre ya está registrado. Por favor elige otro.");

                    ViewBag.Perfil = usuarioActual.idPerfil;
                    ViewBag.Rubros = new SelectList(db.Rubro, "idRubro", "nombreRubro");
                    ViewBag.TiposNegocio = new SelectList(db.TipoDeNegocio, "idTipoNegocio", "nombre", neg.idTipoNegocio);
                    ViewBag.CaracHotel = new SelectList(db.Caracteristica, "idCaracteristica", "nombre");
                    ViewBag.Categorias = new SelectList(db.CategoriaHospedaje, "idCategoria", "nombre");

                    return RedirectToAction("EditHospedaje", new { idNegocio = neg.idNegocio });
                }
            }

            return RedirectToAction("Index", "Home");
        }
        public ActionResult EditComercio(int? idNegocio)
        {
            ObtenerUsuarioActual();
            neg = nm.GetNegocioById((int)idNegocio);

            ViewBag.Perfil = usuarioActual.idPerfil;
            ViewBag.Rubros = new SelectList(db.Rubro, "idRubro", "nombreRubro", neg.Comercio.FirstOrDefault().idRubro);
            ViewBag.idNegocio = idNegocio;

            neg.Sucursal.FirstOrDefault().Domicilio.listLocalidadesCercanas = dm.GetLocalidadesCercanas();
   
            return View(neg);
        }
        [HttpPost]
        public ActionResult EditComercio(NegocioEntity negocio,
                                  [Bind(Include = "idRubro")] ComercioEntity comercio,
                                  [Bind(Include = "localidadSeleccionada,barrio,calle,numero,dpto")] DomicilioEntity domEn,
                                  string telefono,
                                  HttpPostedFileBase imagenPrinc,
                                  HttpPostedFileBase imagenMuestra1,
                                  HttpPostedFileBase imagenMuestra2,
                                  HttpPostedFileBase imagenMuestra3,
                                  HttpPostedFileBase imagenMuestra4,
                                  HttpPostedFileBase imagenMuestra5,
                                  HttpPostedFileBase imagenMuestra6,
                                  int idNegocioOrig)
        {
            ObtenerUsuarioActual();

            neg.idNegocioModif = idNegocioOrig;
            neg.idUsuario = usuarioActual.idUsuario;
            neg.idTipoNegocio = 2;
            neg.nombre = negocio.nombre;
            neg.descripcion = negocio.descripcion;
            neg.Comercio = new List<ComercioEntity>() { comercio };

            neg.Sucursal.Add(new SucursalEntity()
            {
                esPrincipal = true,
                telefono = telefono,
                Domicilio = domEn
            });

            byte[] buffer = null;
            using (var binaryReader = new BinaryReader(imagenPrinc.InputStream))
            {
                buffer = binaryReader.ReadBytes(imagenPrinc.ContentLength);
            }

            FotosNegocio fotoPrinc = new FotosNegocio()
            {
                foto = buffer,
                esPrincipal = true
            };

            List<HttpPostedFileBase> listaHttp = new List<HttpPostedFileBase>() { imagenMuestra1, imagenMuestra2, imagenMuestra3, imagenMuestra4, imagenMuestra5, imagenMuestra6 };

            List<FotosNegocio> fotosNegocio = convertirImagenesMuestra(listaHttp);
            fotosNegocio.Add(fotoPrinc);

            neg.FotosNegocio = fotosNegocio;

            if (ModelState.IsValid)
            {
                nm.AddNegocio(neg, usuarioActual);
            }
            else
            {
                ViewBag.Perfil = usuarioActual.idPerfil;
                ViewBag.Rubros = new SelectList(db.Rubro, "idRubro", "nombreRubro", neg.Comercio.FirstOrDefault().idRubro);
                ViewBag.TiposNegocio = new SelectList(db.TipoDeNegocio, "idTipoNegocio", "nombre", neg.idTipoNegocio);
                return View("EditComercio/"+idNegocioOrig, neg);
            }

            return RedirectToAction("Index", "Home");
        }
        public List<FotosNegocio> convertirImagenesMuestra(List<HttpPostedFileBase> listaHttp)
        {
            List<FotosNegocio> lista = new List<FotosNegocio>();

            foreach (var item in listaHttp)
	        {
                if (item != null)
                {
                    byte[] buffer = null;
                    using (var binaryReader = new BinaryReader(item.InputStream))
                    {
                        buffer = binaryReader.ReadBytes(item.ContentLength);
                    }

                    FotosNegocio fotoMuestra = new FotosNegocio()
                    {
                        foto = buffer,
                        esPrincipal = false
                    };

                    lista.Add(fotoMuestra);
                }
	        }

            return lista;
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