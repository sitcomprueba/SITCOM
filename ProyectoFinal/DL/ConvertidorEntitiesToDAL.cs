using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinesEntities;
using DAL;

namespace BL
{
    public class ConvertidorEntitiesToDAL
    {
        public Comercio ComercioEntityToComercio(ComercioEntity comEn)
        {
            Comercio com = new Comercio()
            {
                idComercio = comEn.idComercio,
                idNegocio = comEn.idNegocio,
                idRubro = comEn.idRubro
            };

            return com;
        }
        public LugarHospedaje LugarHospedajeEntityToLugarHospedaje(LugarHospedajeEntity lugEn)
        {
            LugarHospedaje lug = new LugarHospedaje();
            if (lugEn != null)
            {
                lug.idLugarHospedaje = lugEn.idLugarHospedaje;
                lug.idTipoLugarHospedaje = lugEn.idTipoLugarHospedaje;
                lug.idNegocio = lugEn.idNegocio;

                if (lugEn.CaracteristicasHospedaje.Count != 0)
                    lug.CaracteristicasHospedaje = CaracteristicasHospedajeEntityToCaracteristicasHospedaje(lugEn.CaracteristicasHospedaje);

                if (lugEn.Hotel.Count != 0)
                    lug.Hotel = HotelEntityToHotel(lugEn.Hotel);

                if (lugEn.Complejo.Count != 0)
                    lug.Complejo = ComplejoEntityToComplejo(lugEn.Complejo);

                if(lugEn.CasaDptoOCabana.Count != 0)
                    lug.CasaDptoOCabana = lugEn.CasaDptoOCabana;
            };

            return lug;
        }
        public List<Hotel> HotelEntityToHotel(ICollection<HotelEntity> hot)
        {
            List<Hotel> hotel = new List<Hotel>();

            if (hot != null)
            {
                hotel.Add(new Hotel()
                {
                    idHotel = hot.FirstOrDefault().idHotel,
                    idCategoria = hot.FirstOrDefault().idCategoria,
                    idLugarHospedaje = hot.FirstOrDefault().idLugarHospedaje,
                    Habitacion = hot.FirstOrDefault().Habitacion
                });
            }

            return hotel;
        }
        public List<Complejo> ComplejoEntityToComplejo(ICollection<ComplejoEntity> com)
        {
            List<Complejo> complejo = new List<Complejo>();

            if (com != null)
            {
                complejo.Add(new Complejo()
                {
                    idComplejo = com.FirstOrDefault().idComplejo,
                    idLugarHospedaje = com.FirstOrDefault().idLugarHospedaje,
                    idTipoComplejo = com.FirstOrDefault().idTipoComplejo,
                    idCategoria = com.FirstOrDefault().idCategoria,
                    CasaDptoOCabana = com.FirstOrDefault().CasaDptoOCabana
                });
            }
            return complejo;
        }
        public List<CaracteristicasHospedaje> CaracteristicasHospedajeEntityToCaracteristicasHospedaje(ICollection<CaracteristicasHospedajeEntity> carac)
        {
            List<CaracteristicasHospedaje> caracteristicas = new List<CaracteristicasHospedaje>();
            if (carac != null)
            {
                foreach (var item in carac)
                {
                    caracteristicas.Add(new CaracteristicasHospedaje()
                    {
                        idCaracteristica = item.idCaracteristica,
                        idLugarHospedaje = item.idLugarHospedaje,
                        idCaracteristicaHospedaje = item.idCaracteristicaHospedaje,
                        Caracteristica = item.Caracteristica
                    });
                }
            }
            return caracteristicas;
        }
        public Domicilio DomicilioEntityToDomicilio(DomicilioEntity domEn)
        {
            var dom = new Domicilio()
            {
                calle = domEn.calle,
                barrio = domEn.barrio,
                dpto = domEn.dpto,
                idLocalidad = domEn.localidadSeleccionada,
                numero = domEn.numero
            };

            return dom;
        }
        public Persona PersonaEntityToPersona(PersonaEntity per)
        {
            var persona = new Persona()
            {
                nombre = per.nombre,
                apellido = per.apellido,
                idSexo = per.idSexo,
                idTipoDocumento = per.idTipoDocumento,
                documento = per.documento,
                email = per.email,
                Domicilio = DomicilioEntityToDomicilio(per.Domicilio)
            };

            return persona;
        }
        public Usuarios UsuarioEntityToUsuarios(UsuarioEntity us)
        {
            if (us != null)
            {
                Usuarios user = new Usuarios()
                {
                    idUsuario = us.idUsuario,
                    nombreUsuario = us.nombreUsuario,
                    password = us.password,
                    idPerfil = us.idPerfil
                };

                return user;
            }

            return new Usuarios();
        }
        public List<Sucursal> SucursalEntityToSucursal(SucursalEntity sucEn)
        {
            List<Sucursal> sucursales = new List<Sucursal>();

            if (sucEn != null)
            {
                sucursales.Add(new Sucursal()
                               {
                                   esPrincipal = sucEn.esPrincipal,
                                   Domicilio = DomicilioEntityToDomicilio(sucEn.Domicilio),
                                   telefono = sucEn.telefono
                               });
            }

            return sucursales;
        }

        public Solicitud SolicitudEntityToSolicitud(SolicitudEntity sol)
        {
            Solicitud solicitud = new Solicitud()
            {
                fechaDesde = sol.fechaDesde,
                fechaHasta = sol.fechaHasta,
                cantidadPersonas = sol.cantidadPersonas,
                cantidadLugares = sol.cantidadLugares,
                idNegocio = sol.idNegocio,
                idUsuarioSolicitante = sol.idUsuarioSolicitante,
                observacion = sol.observacion
            };

            return solicitud;
        }

    }
}
