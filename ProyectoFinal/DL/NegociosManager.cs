using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinesEntities;
using DAL;
using System.IO;
using System.Data.SqlClient;

namespace BL
{
    public class NegociosManager
    {
        ConvertidorEntitiesToDAL convert = new ConvertidorEntitiesToDAL();
        public void AddNegocio(NegocioEntity n, UsuarioEntity usuarioActual)
        {
            List<Comercio> lstCom = null;
            List<LugarHospedaje> lstLugar = null;

            if (n.Comercio.FirstOrDefault() != null)
            {
                Comercio com = convert.ComercioEntityToComercio(n.Comercio.FirstOrDefault());
                lstCom = new List<Comercio>() { com };
            }

            if (n.LugarHospedaje.FirstOrDefault() != null)
            {
                LugarHospedaje lug = convert.LugarHospedajeEntityToLugarHospedaje(n.LugarHospedaje.FirstOrDefault());
                lstLugar = new List<LugarHospedaje>() { lug };
            }

            using (SitcomEntities db = new SitcomEntities())
            {
                int idEstTra = 0;
                if (n.idNegocioModif != null) //Seteo el estado de tramite segun si es Alta o Modificacion de Negocio.
                    idEstTra = 3;
                else
                    idEstTra = 1;

                Tramite tra = new Tramite()
                {
                    idUsuarioSolicitante = usuarioActual.idUsuario,
                    fechaAlta = DateTime.Now,
                    idTipoTramite = idEstTra,
                    idEstadoTramite = 1
                };
                var neg = new Negocio()
                {
                    nombre = n.nombre,
                    descripcion = n.descripcion,
                    idTipoNegocio = n.idTipoNegocio,
                    idUsuario = n.idUsuario,
                    Comercio = lstCom,
                    LugarHospedaje = lstLugar,
                    FotosNegocio = n.FotosNegocio,
                    Tramite = new List<Tramite>() { tra },
                    Sucursal = convert.SucursalEntityToSucursal(n.Sucursal.FirstOrDefault()),
                    estaAprobado = false,
                    idNegocioModif = n.idNegocioModif
                };
                db.Negocio.Add(neg);
                db.SaveChanges();

            }
        }
        public FotosNegocio GetFotoNegocioById(int id)
        {
            using (var db = new SitcomEntities())
            {
                var result = (from f in db.FotosNegocio
                              where f.idFoto == id
                              select f).FirstOrDefault();

                return result;
            }
        }
        public List<NegocioEntity> GetAllNegocios(int tipoNegocio)
        {

            using (var db = new SitcomEntities())
            {
                var result = (from n in db.Negocio
                              where n.estaAprobado == true
                              && n.idTipoNegocio == tipoNegocio
                              select new NegocioEntity()
                              {
                                  idNegocio = n.idNegocio,
                                  nombre = n.nombre,
                                  descripcion = n.descripcion,
                                  idTipoNegocio = n.idTipoNegocio,
                                  idUsuario = n.idUsuario,
                                  fechaAlta = DateTime.Today,
                                  Comercio = (from c in db.Comercio
                                              where c.idNegocio == n.idNegocio
                                              select new ComercioEntity()
                                              {
                                                  idComercio = c.idComercio,
                                                  idRubro = c.idRubro,
                                                  idNegocio = c.idNegocio,
                                                  Rubro = db.Rubro.Where(r => r.idRubro == c.idRubro).FirstOrDefault()
                                              }).ToList(),
                                  LugarHospedaje = (from l in db.LugarHospedaje
                                                    where l.idNegocio == n.idNegocio
                                                    select new LugarHospedajeEntity()
                                                    {
                                                        idLugarHospedaje = l.idLugarHospedaje,
                                                        idTipoLugarHospedaje = l.idTipoLugarHospedaje,
                                                        TipoLugarHospedaje = (from tl in db.TipoLugarHospedaje
                                                                              where tl.idTipoLugarHospedaje == l.idTipoLugarHospedaje
                                                                              select tl).FirstOrDefault(),
                                                        idNegocio = l.idNegocio,
                                                        Hotel = (from h in db.Hotel
                                                                 where h.idLugarHospedaje == l.idLugarHospedaje
                                                                 select new HotelEntity
                                                                 {
                                                                     idHotel = h.idHotel,
                                                                     idCategoria = h.idCategoria,
                                                                     idLugarHospedaje = h.idLugarHospedaje,
                                                                     CategoriaHospedaje = db.CategoriaHospedaje.Where(cat => cat.idCategoria == h.idCategoria).FirstOrDefault()
                                                                 }).ToList(),
                                                        Complejo = (from com in db.Complejo
                                                                    where com.idLugarHospedaje == l.idLugarHospedaje
                                                                    select new ComplejoEntity
                                                                    {
                                                                        idComplejo = com.idComplejo,
                                                                        idLugarHospedaje = com.idLugarHospedaje,
                                                                        idCategoria = com.idCategoria,
                                                                        idTipoComplejo = com.idTipoComplejo,
                                                                        TipoComplejo = db.TipoComplejo.Where(tip => tip.idTipoComplejo == com.idTipoComplejo).FirstOrDefault(),
                                                                        CategoriaHospedaje = db.CategoriaHospedaje.Where(cat => cat.idCategoria == com.idCategoria).FirstOrDefault()
                                                                    }).ToList(),
                                                        CasaDptoOCabana = (from cdo in db.CasaDptoOCabana
                                                                           where cdo.idLugarHospedaje == l.idLugarHospedaje
                                                                           select cdo).ToList()
                                                    }).ToList(),
                                  FotosNegocio = (from f in db.FotosNegocio
                                                  where f.idNegocio == n.idNegocio
                                                  select f).ToList(),
                                  Sucursal = (from s in db.Sucursal
                                              where s.idNegocio == n.idNegocio
                                              select new SucursalEntity()
                                              {
                                                  idSucursal = s.idSucursal,
                                                  esPrincipal = s.esPrincipal,
                                                  Domicilio = (from d in db.Domicilio
                                                               where d.idDomicilio == s.idDomicilio
                                                               select new DomicilioEntity()
                                                               {
                                                                   idDomicilio = d.idDomicilio,
                                                                   idLocalidad = d.idLocalidad,
                                                                   calle = d.calle,
                                                                   numero = d.numero,
                                                                   dpto = d.dpto,
                                                                   barrio = d.barrio,
                                                                   Localidad = db.Localidad.Where(loc => loc.idLocalidad == d.idLocalidad).FirstOrDefault()
                                                               }).FirstOrDefault(),
                                                  nombreSucursal = s.nombreSucursal,
                                                  telefono = s.telefono
                                              }).ToList()
                              }).ToList<NegocioEntity>();

                return result;
            }
        }
        public List<NegocioEntity> GetAllLugaresHospedaje()
        {
            using (var db = new SitcomEntities())
            {
                var result = (from n in db.Negocio
                              where n.estaAprobado == true
                              && n.idTipoNegocio == 1
                              select new NegocioEntity()
                              {
                                  idNegocio = n.idNegocio,
                                  nombre = n.nombre,
                                  descripcion = n.descripcion,
                                  idTipoNegocio = n.idTipoNegocio,
                                  idUsuario = n.idUsuario,
                                  fechaAlta = DateTime.Today,
                                  Comercio = (from c in db.Comercio
                                              where c.idNegocio == n.idNegocio
                                              select new ComercioEntity()
                                              {
                                                  idComercio = c.idComercio,
                                                  idRubro = c.idRubro,
                                                  idNegocio = c.idNegocio,
                                                  Rubro = db.Rubro.Where(r => r.idRubro == c.idRubro).FirstOrDefault()
                                              }).ToList(),
                                  LugarHospedaje = (from l in db.LugarHospedaje
                                                    where l.idNegocio == n.idNegocio
                                                    select new LugarHospedajeEntity()
                                                    {
                                                        idLugarHospedaje = l.idLugarHospedaje,
                                                        idTipoLugarHospedaje = l.idTipoLugarHospedaje,
                                                        TipoLugarHospedaje = (from tl in db.TipoLugarHospedaje
                                                                              where tl.idTipoLugarHospedaje == l.idTipoLugarHospedaje
                                                                              select tl).FirstOrDefault(),
                                                        idNegocio = l.idNegocio,
                                                        Hotel = (from h in db.Hotel
                                                                 where h.idLugarHospedaje == l.idLugarHospedaje
                                                                 select new HotelEntity
                                                                 {
                                                                     idHotel = h.idHotel,
                                                                     idCategoria = h.idCategoria,
                                                                     idLugarHospedaje = h.idLugarHospedaje,
                                                                     CategoriaHospedaje = db.CategoriaHospedaje.Where(cat => cat.idCategoria == h.idCategoria).FirstOrDefault()
                                                                 }).ToList(),
                                                        Complejo = (from com in db.Complejo
                                                                    where com.idLugarHospedaje == l.idLugarHospedaje
                                                                    select new ComplejoEntity
                                                                    {
                                                                        idComplejo = com.idComplejo,
                                                                        idLugarHospedaje = com.idLugarHospedaje,
                                                                        idCategoria = com.idCategoria,
                                                                        idTipoComplejo = com.idTipoComplejo,
                                                                        TipoComplejo = db.TipoComplejo.Where(tip => tip.idTipoComplejo == com.idTipoComplejo).FirstOrDefault(),
                                                                        CategoriaHospedaje = db.CategoriaHospedaje.Where(cat => cat.idCategoria == com.idCategoria).FirstOrDefault()
                                                                    }).ToList(),
                                                        CasaDptoOCabana = (from cdo in db.CasaDptoOCabana
                                                                           where cdo.idLugarHospedaje == l.idLugarHospedaje
                                                                           select cdo).ToList()
                                                    }).ToList(),
                                  FotosNegocio = (from f in db.FotosNegocio
                                                  where f.idNegocio == n.idNegocio
                                                  select f).ToList(),
                                  Sucursal = (from s in db.Sucursal
                                              where s.idNegocio == n.idNegocio
                                              select new SucursalEntity()
                                              {
                                                  idSucursal = s.idSucursal,
                                                  esPrincipal = s.esPrincipal,
                                                  Domicilio = (from d in db.Domicilio
                                                               where d.idDomicilio == s.idDomicilio
                                                               select new DomicilioEntity()
                                                               {
                                                                   idDomicilio = d.idDomicilio,
                                                                   idLocalidad = d.idLocalidad,
                                                                   calle = d.calle,
                                                                   numero = d.numero,
                                                                   dpto = d.dpto,
                                                                   barrio = d.barrio,
                                                                   Localidad = db.Localidad.Where(loc => loc.idLocalidad == d.idLocalidad).FirstOrDefault()
                                                               }).FirstOrDefault(),
                                                  nombreSucursal = s.nombreSucursal,
                                                  telefono = s.telefono
                                              }).ToList()
                              }).ToList<NegocioEntity>();

                return result;
            }
        }
        public bool ValidarExisteNegocio(string nom, int? idNegocioModif)
        {
            using (var db = new SitcomEntities())
            {
                List<int> estados = new List<int>() { 1, 2 };
                /* Busca si hay un negocio con el mismo nombre y aprobado
                 * O si tiene el mismo nombre y más alla de no estar aprobado, todavía esta
                 * en revisión o abierto el tramite del mismo. */
                if (idNegocioModif != null)
                {
                    var negocioOrig = db.Negocio.Where(n => n.idNegocio == idNegocioModif).FirstOrDefault();

                    var result = (from n in db.Negocio
                                  where (n.estaAprobado == true && n.nombre == nom && n.nombre != negocioOrig.nombre)
                                      || (n.nombre == nom && estados.Contains((int)n.Tramite.FirstOrDefault().idEstadoTramite) && n.nombre != negocioOrig.nombre)
                                  select n).FirstOrDefault();

                    if (result != null)
                        return false;
                    else
                        return true;
                }
                else
                {
                    var result = (from n in db.Negocio
                                  where (n.estaAprobado == true && n.nombre == nom)
                                      || (n.nombre == nom && estados.Contains((int)n.Tramite.FirstOrDefault().idEstadoTramite))
                                  select n).FirstOrDefault();

                    if (result != null)
                        return false;
                    else
                        return true;
                }


            }
        }
        public NegocioEntity GetNegocioByNombre(string nom)
        {
            using (var db = new SitcomEntities())
            {
                var result = (from n in db.Negocio
                              where n.nombre == nom
                              select new NegocioEntity()
                              {
                                  nombre = n.nombre,
                                  descripcion = n.descripcion,
                                  idTipoNegocio = n.idTipoNegocio,
                                  idUsuario = n.idUsuario,
                                  fechaAlta = DateTime.Today,
                                  LugarHospedaje = (from l in db.LugarHospedaje
                                                    where l.idNegocio == n.idNegocio
                                                    select new LugarHospedajeEntity()
                                                    {
                                                        idLugarHospedaje = l.idLugarHospedaje,
                                                        idTipoLugarHospedaje = l.idTipoLugarHospedaje,
                                                        idNegocio = l.idNegocio
                                                    }).ToList(),
                                  Comercio = (from c in db.Comercio
                                              where c.idNegocio == n.idNegocio
                                              select new ComercioEntity()
                                              {
                                                  idComercio = c.idComercio,
                                                  idRubro = c.idRubro,
                                                  idNegocio = c.idNegocio
                                              }).ToList()
                              }).FirstOrDefault();

                return result;
            }
        }
        public NegocioEntity GetNegocioById(int id)
        {
            using (var db = new SitcomEntities())
            {
                var result = (from n in db.Negocio
                              where n.idNegocio == id
                              select new NegocioEntity()
                              {
                                  idNegocio = n.idNegocio,
                                  nombre = n.nombre,
                                  descripcion = n.descripcion,
                                  idTipoNegocio = n.idTipoNegocio,
                                  TipoDeNegocio = (from t in db.TipoDeNegocio
                                                   where t.idTipoNegocio == n.idTipoNegocio
                                                   select t).FirstOrDefault(),
                                  idUsuario = n.idUsuario,
                                  Usuarios = (from u in db.Usuarios
                                              where u.idUsuario == n.idUsuario
                                              select u).FirstOrDefault(),
                                  fechaAlta = DateTime.Today,
                                  FotosNegocio = (from f in db.FotosNegocio
                                                  where f.idNegocio == id
                                                  select f).ToList(),
                                  LugarHospedaje = (from l in db.LugarHospedaje
                                                    where l.idNegocio == n.idNegocio
                                                    select new LugarHospedajeEntity()
                                                    {
                                                        idLugarHospedaje = l.idLugarHospedaje,
                                                        idTipoLugarHospedaje = l.idTipoLugarHospedaje,
                                                        TipoLugarHospedaje = (from tl in db.TipoLugarHospedaje
                                                                              where tl.idTipoLugarHospedaje == l.idTipoLugarHospedaje
                                                                              select tl).FirstOrDefault(),
                                                        idNegocio = l.idNegocio,
                                                        moduloReservas = l.moduloReservas,
                                                        Hotel = (from h in db.Hotel
                                                                 where h.idLugarHospedaje == l.idLugarHospedaje
                                                                 select new HotelEntity
                                                                 {
                                                                     idHotel = h.idHotel,
                                                                     idCategoria = h.idCategoria,
                                                                     idLugarHospedaje = h.idLugarHospedaje,
                                                                     CategoriaHospedaje = db.CategoriaHospedaje.Where(cat => cat.idCategoria == h.idCategoria).FirstOrDefault(),
                                                                     Habitacion = db.Habitacion.Where(hab => hab.idHotel == h.idHotel).ToList()
                                                                 }).ToList(),
                                                        Complejo = (from com in db.Complejo
                                                                    where com.idLugarHospedaje == l.idLugarHospedaje
                                                                    select new ComplejoEntity
                                                                    {
                                                                        idComplejo = com.idComplejo,
                                                                        idLugarHospedaje = com.idLugarHospedaje,
                                                                        idCategoria = com.idCategoria,
                                                                        idTipoComplejo = com.idTipoComplejo,
                                                                        TipoComplejo = db.TipoComplejo.Where(tip => tip.idTipoComplejo == com.idTipoComplejo).FirstOrDefault(),
                                                                        CategoriaHospedaje = db.CategoriaHospedaje.Where(cat => cat.idCategoria == com.idCategoria).FirstOrDefault()
                                                                    }).ToList(),
                                                        CasaDptoOCabana = (from cdo in db.CasaDptoOCabana
                                                                           where cdo.idLugarHospedaje == l.idLugarHospedaje
                                                                           select cdo).ToList(),
                                                        CaracteristicasHospedaje = (from ch in db.CaracteristicasHospedaje
                                                                                    where ch.idLugarHospedaje == l.idLugarHospedaje
                                                                                    select new CaracteristicasHospedajeEntity()
                                                                                    {
                                                                                        idCaracteristicaHospedaje = ch.idCaracteristicaHospedaje,
                                                                                        idCaracteristica = ch.idCaracteristica,
                                                                                        idLugarHospedaje = ch.idLugarHospedaje,
                                                                                        Caracteristica = db.Caracteristica.Where(car => car.idCaracteristica == ch.idCaracteristica).FirstOrDefault()
                                                                                    }).ToList()
                                                    }).ToList(),
                                  Comercio = (from c in db.Comercio
                                              where c.idNegocio == n.idNegocio
                                              select new ComercioEntity()
                                              {
                                                  idComercio = c.idComercio,
                                                  idRubro = c.idRubro,
                                                  Rubro = (from r in db.Rubro
                                                           where r.idRubro == c.idRubro
                                                           select r).FirstOrDefault(),
                                                  idNegocio = c.idNegocio
                                              }).ToList(),
                                  Tramite = (from t in db.Tramite
                                             where t.idNegocio == n.idNegocio
                                             select new TramiteEntity()
                                             {
                                                 idTramite = t.idTramite,
                                                 idEstadoTramite = t.idEstadoTramite,
                                                 idTipoTramite = t.idTipoTramite,
                                                 idUsuarioResponsable = t.idUsuarioResponsable,
                                                 idNegocio = t.idNegocio,
                                                 idUsuarioSolicitante = t.idUsuarioSolicitante,
                                                 comentario = t.comentario
                                             }).ToList(),
                                  Sucursal = (from s in db.Sucursal
                                              where s.idNegocio == n.idNegocio
                                              select new SucursalEntity()
                                              {
                                                  idSucursal = s.idSucursal,
                                                  esPrincipal = s.esPrincipal,
                                                  Domicilio = (from d in db.Domicilio
                                                               where d.idDomicilio == s.idDomicilio
                                                               select new DomicilioEntity()
                                                               {
                                                                   idDomicilio = d.idDomicilio,
                                                                   idLocalidad = d.idLocalidad,
                                                                   calle = d.calle,
                                                                   numero = d.numero,
                                                                   dpto = d.dpto,
                                                                   barrio = d.barrio,
                                                                   Localidad = db.Localidad.Where(loc => loc.idLocalidad == d.idLocalidad).FirstOrDefault()
                                                               }).FirstOrDefault(),
                                                  nombreSucursal = s.nombreSucursal,
                                                  telefono = s.telefono
                                              }).ToList()
                              }).FirstOrDefault();

                return result;
            }
        }
        public List<Negocio> GetNegocioByUsuario(int id)
        {
            using (var db = new SitcomEntities())
            {
                var result = db.Negocio.Include("TipoDeNegocio").Where(n => n.idUsuario == id && n.estaAprobado == true).ToList();

                return result;
            }
        }
        public List<Caracteristica> GetCaracteristicas()
        {
            using (var db = new SitcomEntities())
            {
                var result = (from c in db.Caracteristica
                              select c).ToList();

                return result;
            }
        }
        public List<TipoHabitacion> GetTiposHabitacion()
        {
            using (var db = new SitcomEntities())
            {
                var result = db.TipoHabitacion.ToList();

                return result;
            }
        }

        public void UpdateDptosOCabanasCambio(int idNegocio)
        {
            using (SitcomEntities db = new SitcomEntities())
            {

                SqlParameter paramNegocio = new SqlParameter("@pNegocio", idNegocio);

                db.Database.ExecuteSqlCommand("cambiarCasaODptoNuevoComplejo @idNegocioNuevo=@pNegocio", paramNegocio);
            }

        }

        public void UpdateHabitacionesCambio(int idNegocio)
        {
            using (SitcomEntities db = new SitcomEntities())
            {

                SqlParameter paramNegocio = new SqlParameter("@pNegocio", idNegocio);

                db.Database.ExecuteSqlCommand("cambiarHabitacionesNuevoHotel @idNegocioNuevo=@pNegocio", paramNegocio);
            }

        }
    }
}

