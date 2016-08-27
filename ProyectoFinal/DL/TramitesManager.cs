using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinesEntities;
using DAL;

namespace BL
{
    public class TramitesManager
    {
        private NegociosManager nm = new NegociosManager();
        public List<TramiteEntity> GetAllTramites(int? id)
        {
            if (id == null)
                id = 0;

            using (var db = new SitcomEntities())
            {
                if (id == 0)
                {
                    var result = (from t in db.Tramite
                                  select new TramiteEntity()
                                  {
                                      idTramite = t.idTramite,
                                      idUsuarioSolicitante = t.idUsuarioSolicitante,
                                      Usuarios = (from u in db.Usuarios
                                                  where u.idUsuario == t.idUsuarioSolicitante
                                                  select u).FirstOrDefault(),
                                      idUsuarioResponsable=t.idUsuarioResponsable,
                                      Usuarios1 = (from u in db.Usuarios
                                                  where u.idUsuario == t.idUsuarioResponsable
                                                  select u).FirstOrDefault(),
                                      fechaAlta = t.fechaAlta,
                                      fechaFin = t.fechaFin,
                                      idNegocio=t.idNegocio,
                                      Negocio = (from n in db.Negocio
                                                 where n.idNegocio == t.idNegocio
                                                 select n).FirstOrDefault(),
                                      idEstadoTramite = t.idEstadoTramite,
                                      EstadoTramite = (from e in db.EstadoTramite
                                                       where e.idEstadoTramite == t.idEstadoTramite
                                                       select e).FirstOrDefault(),
                                      idTipoTramite = t.idTipoTramite,
                                      TipoTramite = (from tip in db.TipoTramite
                                                     where tip.idTipoTramite == t.idTipoTramite
                                                     select tip).FirstOrDefault()
                                  }).ToList();
                    return result;
                }
                else
                {
                    var result = (from t in db.Tramite
                                  where t.idEstadoTramite == t.idEstadoTramite
                                  select new TramiteEntity()
                                  {
                                      idTramite = t.idTramite,
                                      idUsuarioSolicitante = t.idUsuarioSolicitante,
                                      Usuarios = (from u in db.Usuarios
                                                  where u.idUsuario == t.idUsuarioSolicitante
                                                  select u).FirstOrDefault(),
                                      idUsuarioResponsable = t.idUsuarioResponsable,
                                      Usuarios1 = (from u in db.Usuarios
                                                   where u.idUsuario == t.idUsuarioResponsable
                                                   select u).FirstOrDefault(),
                                      fechaAlta = t.fechaAlta,
                                      fechaFin = t.fechaFin,
                                      Negocio = (from n in db.Negocio
                                                 where n.idNegocio == t.idNegocio
                                                 select n).FirstOrDefault(),
                                      EstadoTramite = (from e in db.EstadoTramite
                                                       where e.idEstadoTramite == t.idEstadoTramite
                                                       select e).FirstOrDefault(),
                                      TipoTramite = (from tip in db.TipoTramite
                                                     where tip.idTipoTramite == t.idTipoTramite
                                                     select tip).FirstOrDefault()
                                  }).ToList();
                    return result;
                }

            };


        }
        public TramiteEntity GetTramiteById(int id)
        {
            using (var db = new SitcomEntities())
            {

                var result = (from t in db.Tramite
                              where t.idTramite == id
                              select new TramiteEntity()
                              {
                                  idTramite = t.idTramite,
                                  idUsuarioSolicitante = t.idUsuarioSolicitante,
                                  Usuarios = (from u in db.Usuarios
                                              where u.idUsuario == t.idUsuarioSolicitante
                                              select u).FirstOrDefault(),
                                  fechaAlta = t.fechaAlta,
                                  fechaFin = t.fechaFin,
                                  idTipoTramite=t.idTipoTramite,
                                  idEstadoTramite=t.idEstadoTramite,
                                  comentario = t.comentario,
                                  idNegocio=t.idNegocio,
                                  idUsuarioResponsable=t.idUsuarioResponsable,
                                  Negocio = (from n in db.Negocio
                                             where n.idNegocio == t.idNegocio
                                             select n).FirstOrDefault(),
                                  EstadoTramite = (from e in db.EstadoTramite
                                                   where e.idEstadoTramite == t.idEstadoTramite
                                                   select e).FirstOrDefault(),
                                  TipoTramite = (from tip in db.TipoTramite
                                                 where tip.idTipoTramite == t.idTipoTramite
                                                 select tip).FirstOrDefault()
                              }).FirstOrDefault();

                return result;
            }
        }
        public Tramite TramiteEntityToTramite(TramiteEntity tra)
        {
            Tramite t = new Tramite()
            {
                idTramite = tra.idTramite,
                idEstadoTramite = tra.idEstadoTramite,
                idTipoTramite = tra.idTipoTramite,
                idUsuarioSolicitante = tra.idUsuarioSolicitante,
                idUsuarioResponsable = tra.idUsuarioResponsable,
                idNegocio = tra.idNegocio,
                fechaAlta = tra.fechaAlta,
                fechaFin = tra.fechaFin,
                EstadoTramite = tra.EstadoTramite,
                Negocio = tra.Negocio,
                Usuarios = tra.Usuarios
            };

            return t;
        }
        //public void TomarTramite(int id, UsuarioEntity us)
        //{
        //    using (SitcomEntities db = new SitcomEntities())
        //    {
        //        var result = (from t in db.Tramite
        //                      where t.idTramite == id
        //                      select t).FirstOrDefault();

        //        if (result != null)
        //        {
        //            result.idUsuarioResponsable = us.idUsuario;
        //            result.idEstadoTramite = 2;

        //            db.SaveChanges();
        //        }
                          
        //    };
        //}
        public void CambiarEstadoTramite(TramiteEntity tramite, int idEstadoACambiar, UsuarioEntity us)
        {
            switch (tramite.idTipoTramite)
           	{
                case 1: CETAltaNegocio(tramite,idEstadoACambiar,us);
                        break;
                case 2: CETModuloReservas(tramite, idEstadoACambiar, us);
                        break;
                case 3: CETModifNegocio(tramite, idEstadoACambiar, us);
                        break;
	        	
                default:
                break;
	        }
        }
        public void CETAltaNegocio(TramiteEntity tramite, int idEstadoACambiar, UsuarioEntity us)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                var result = db.Tramite.Include("Negocio").Where(t => t.idTramite == tramite.idTramite).FirstOrDefault();

                if (result != null)
                {
                    switch (idEstadoACambiar)
                    {
                        case 2: result.idUsuarioResponsable = us.idUsuario;
                            result.idEstadoTramite = 2;//2: En revisión

                            db.SaveChanges();
                            break;
                        case 3: result.fechaFin = DateTime.Now; //3: Aprobado
                            result.idEstadoTramite = 3;
                            result.Negocio.estaAprobado = true;
                            result.comentario = tramite.comentario;

                            db.SaveChanges();
                            break;
                        case 4: result.fechaFin = DateTime.Now;//4: Rechazado
                            result.idEstadoTramite = 4;
                            result.Negocio.estaAprobado = false;
                            result.comentario = tramite.comentario;

                            db.SaveChanges();
                            break;
                        case 5: result.fechaFin = DateTime.Now;
                            result.idEstadoTramite = 5; //5: Cancelado

                            db.SaveChanges();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        public void CETModifNegocio(TramiteEntity tramite, int idEstadoACambiar, UsuarioEntity us)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                var result = db.Tramite.Include("Negocio")
                                       .Include("Negocio.LugarHospedaje")
                                       .Where(t => t.idTramite == tramite.idTramite).FirstOrDefault();

                var negocioOrig = db.Negocio.Where(n => n.idNegocio == result.Negocio.idNegocioModif).FirstOrDefault();

                if (result != null)
                {
                    switch (idEstadoACambiar)
                    {
                        case 2: result.idUsuarioResponsable = us.idUsuario;
                                result.idEstadoTramite = 2;//2: En revisión

                            db.SaveChanges();
                            break;
                        case 3: result.fechaFin = DateTime.Now; //3: Aprobado
                            result.idEstadoTramite = 3;
                            result.Negocio.estaAprobado = true;
                            result.comentario = tramite.comentario;
                            negocioOrig.estaAprobado = false;
                            
                            if(result.Negocio.idTipoNegocio == 1) //Si es lugar de hospedaje
                            {
                                var neg = nm.GetNegocioById(result.Negocio.idNegocio);
                                var negAnt = nm.GetNegocioById((int)result.Negocio.idNegocioModif);

                                if (negAnt.LugarHospedaje.FirstOrDefault().moduloReservas == true)
                                    result.Negocio.LugarHospedaje.FirstOrDefault().moduloReservas = true;
                                switch (neg.LugarHospedaje.FirstOrDefault().idTipoLugarHospedaje)
                                {
                                    case 2: nm.UpdateDptosOCabanasCambio(neg.idNegocio); //ACTUALIZAR CASAS,DPTOS O CABAÑAS
                                        break;
                                    case 3: nm.UpdateHabitacionesCambio(neg.idNegocio); //ACTUALIZAR HABITACIONES
                                        break;
                                    default:
                                        break;
                                }
                            }

                            db.SaveChanges();
                            break;
                        case 4: result.fechaFin = DateTime.Now;//4: Rechazado
                            result.idEstadoTramite = 4;
                            result.Negocio.estaAprobado = false;
                            result.comentario = tramite.comentario;

                            db.SaveChanges();
                            break;
                        case 5: result.fechaFin = DateTime.Now;
                            result.idEstadoTramite = 5; //5: Cancelado

                            db.SaveChanges();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        public void CETModuloReservas(TramiteEntity tramite, int idEstadoACambiar, UsuarioEntity us)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                var result = db.Tramite.Include("Negocio")
                                       .Include("Negocio.LugarHospedaje")
                                       .Where(t => t.idTramite == tramite.idTramite).FirstOrDefault();

                if (result != null)
                {
                    switch (idEstadoACambiar)
                    {
                        case 2: result.idUsuarioResponsable = us.idUsuario;
                                result.idEstadoTramite = 2;//2: En revisión

                                db.SaveChanges();
                                break;
                        case 3: result.fechaFin = DateTime.Now; //3: Aprobado
                                result.idEstadoTramite = 3;
                                result.Negocio.LugarHospedaje.FirstOrDefault().moduloReservas = true;

                                db.SaveChanges();
                                break;
                        case 4: result.fechaFin = DateTime.Now;//4: Rechazado
                                result.idEstadoTramite = 4;
                                result.Negocio.LugarHospedaje.FirstOrDefault().moduloReservas = false;
                                result.comentario = tramite.comentario;

                                db.SaveChanges();
                                break;
                        case 5: result.fechaFin = DateTime.Now;
                                result.idEstadoTramite = 5; //5: Cancelado

                                db.SaveChanges();
                                break;
                        default:
                            break;
                    }
                }
            }
        }

        //public void CancelarTramite(int id)
        //{
        //    using (SitcomEntities db = new SitcomEntities())
        //    {
        //        var result = (from t in db.Tramite
        //                      where t.idTramite == id
        //                      select t).FirstOrDefault();

        //        if (result != null)
        //        {
                    
        //        }
        //    };
        //}
        public List<TramiteEntity> GetTramitesByUsuario(UsuarioEntity us)
        {
            using (var db = new SitcomEntities())
            {
                var result = (from t in db.Tramite
                              where t.idUsuarioSolicitante == us.idUsuario 
                                 || t.idUsuarioResponsable == us.idUsuario
                              select new TramiteEntity()
                              {
                                  idTramite = t.idTramite,
                                  idUsuarioSolicitante = t.idUsuarioSolicitante,
                                  Usuarios = (from u in db.Usuarios
                                              where u.idUsuario == t.idUsuarioSolicitante
                                              select u).FirstOrDefault(),
                                  idUsuarioResponsable = t.idUsuarioResponsable,
                                  Usuarios1 = (from u in db.Usuarios
                                               where u.idUsuario == t.idUsuarioResponsable
                                               select u).FirstOrDefault(),
                                  fechaAlta = t.fechaAlta,
                                  fechaFin = t.fechaFin,
                                  idTipoTramite = t.idTipoTramite,
                                  idEstadoTramite = t.idEstadoTramite,
                                  idNegocio = t.idNegocio,
                                  Negocio = (from n in db.Negocio
                                             where n.idNegocio == t.idNegocio
                                             select n).FirstOrDefault(),
                                  EstadoTramite = (from e in db.EstadoTramite
                                                   where e.idEstadoTramite == t.idEstadoTramite
                                                   select e).FirstOrDefault(),
                                  TipoTramite = (from tip in db.TipoTramite
                                                 where tip.idTipoTramite == t.idTipoTramite
                                                 select tip).FirstOrDefault()
                              }).ToList();

                foreach (var item in result)
                {
                    if (item.idUsuarioResponsable == null)
                        item.Usuarios1 = new Usuarios() { nombreUsuario = "No Asignado" };
                }

                return result;
            }
        }
        public List<TramiteEntity> GetTramitesByNegocio(int id)
        {
            using (var db = new SitcomEntities())
            {
                var result = (from t in db.Tramite
                              where t.idNegocio == id
                              select new TramiteEntity()
                              {
                                  idTramite = t.idTramite,
                                  idUsuarioSolicitante = t.idUsuarioSolicitante,
                                  Usuarios = (from u in db.Usuarios
                                              where u.idUsuario == t.idUsuarioSolicitante
                                              select u).FirstOrDefault(),
                                  idUsuarioResponsable = t.idUsuarioResponsable,
                                  Usuarios1 = (from u in db.Usuarios
                                               where u.idUsuario == t.idUsuarioResponsable
                                               select u).FirstOrDefault(),
                                  fechaAlta = t.fechaAlta,
                                  fechaFin = t.fechaFin,
                                  idTipoTramite = t.idTipoTramite,
                                  idEstadoTramite = t.idEstadoTramite,
                                  idNegocio = t.idNegocio,
                                  Negocio = (from n in db.Negocio
                                             where n.idNegocio == t.idNegocio
                                             select n).FirstOrDefault(),
                                  EstadoTramite = (from e in db.EstadoTramite
                                                   where e.idEstadoTramite == t.idEstadoTramite
                                                   select e).FirstOrDefault(),
                                  TipoTramite = (from tip in db.TipoTramite
                                                 where tip.idTipoTramite == t.idTipoTramite
                                                 select tip).FirstOrDefault()
                              }).ToList();

                foreach (var item in result)
                {
                    if (item.idUsuarioResponsable == null)
                        item.Usuarios1 = new Usuarios() { nombreUsuario = "No Asignado" };
                }

                return result;
            }
        }
    }
}
