using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BussinesEntities;

namespace BL
{
    public class ReservasManager
    {
        ConvertidorEntitiesToDAL convert = new ConvertidorEntitiesToDAL();
        public void SolicitarModuloReservas(int idNegocio, UsuarioEntity usuarioActual)
        {
            using(var db = new SitcomEntities())
            {
                Tramite t = new Tramite()
                {
                    idUsuarioSolicitante = usuarioActual.idUsuario,
                    fechaAlta = DateTime.Now,
                    idTipoTramite = 2,
                    idEstadoTramite = 1,
                    idNegocio = idNegocio
                };

                db.Tramite.Add(t);
                db.SaveChanges();
            }
        }

        public void AddSolicitudReserva(SolicitudEntity solicitud)
        {
            using (var db = new SitcomEntities())
            {
                Solicitud sol = convert.SolicitudEntityToSolicitud(solicitud);

                db.Solicitud.Add(sol);
                db.SaveChanges();
            }
        }

        public List<SolicitudEntity> GetSolicitudesReserva(int idNegocio)
        {
            using(var db = new SitcomEntities())
            {
                var result = (from s in db.Solicitud
                              where s.idNegocio == idNegocio
                              select new SolicitudEntity()
                              {
                                  idSolicitud = s.idSolicitud,
                                  idNegocio = s.idNegocio,
                                  idUsuarioSolicitante = s.idUsuarioSolicitante,
                                  cantidadLugares = s.cantidadLugares,
                                  cantidadPersonas = s.cantidadPersonas,
                                  fechaDesde = s.fechaDesde,
                                  fechaHasta = s.fechaHasta,
                                  observacion = s.observacion,
                                  Usuarios = db.Usuarios.Where(us => us.idUsuario == s.idUsuarioSolicitante).FirstOrDefault()
                              }).ToList();

                return result;
            }
        }
        public SolicitudEntity GetSolicitudReservaById(int idSolicitud)
        {
            using (var db = new SitcomEntities())
            {
                var result = (from s in db.Solicitud
                              where s.idSolicitud == idSolicitud
                              select new SolicitudEntity()
                              {
                                  idSolicitud = s.idSolicitud,
                                  idNegocio = s.idNegocio,
                                  idUsuarioSolicitante = s.idUsuarioSolicitante,
                                  cantidadLugares = s.cantidadLugares,
                                  cantidadPersonas = s.cantidadPersonas,
                                  fechaDesde = s.fechaDesde,
                                  fechaHasta = s.fechaHasta,
                                  observacion = s.observacion,
                                  Usuarios = db.Usuarios.Where(us => us.idUsuario == s.idUsuarioSolicitante).FirstOrDefault()
                              }).FirstOrDefault();

                return result;
            }
        }
    }
}
