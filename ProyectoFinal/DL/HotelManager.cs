    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinesEntities;
using DAL;
using System.Data.SqlClient;


namespace BL
{
  public  class HotelManager
    {


      public Hotel buscarHotelById(int idHot)
      {
          using (var db = new SitcomEntities())
          {
              var result = (from u in db.Hotel
                            where u.idHotel == idHot
                            select u).FirstOrDefault();
                            

              return result;
          }
      }


      public List<Habitacion> getHabitacionesByHotel(int? idHot)
  {
      using (var db = new SitcomEntities())
      {
          var result = (from u in db.Habitacion
                        where u.idHotel == idHot
                        select u).ToList<Habitacion>();


          return result;

      }
  
  
  
  }


      public void registrarDisponibilidad(DisponibilidadEntity dispo)
      {
          using (SitcomEntities db = new SitcomEntities())
          {
              var dis = new Disponibilidad()
              {
                  fechaDesde= dispo.fechaDesde,
                  fechaHasta=dispo.fechaHasta,
                  idHabitacion=dispo.idHabitacion,
                  habilitado=dispo.habilitado,
                  idEstado=dispo.idEstado
                  
              };

              db.Disponibilidad.Add(dis);
              db.SaveChanges();
          }
      }

      public List<DisponibilidadEntity> consultarDisponibilidad(int? idHot, int? cantHab, int? cantPers, int? anio, int? mes)
      {
          List<DisponibilidadEntity> lstDispo = new List<DisponibilidadEntity>();


          using (SitcomEntities db = new SitcomEntities())
          {

              SqlParameter paramComplejo = new SqlParameter("@pHotel", idHot);
              SqlParameter paramCantHab = new SqlParameter("@pCantHab", cantHab);
              SqlParameter paramCantPers = new SqlParameter("@pCantPers", cantPers);
              SqlParameter paramAnio = new SqlParameter("@pAnio", anio);
              SqlParameter paramMes = new SqlParameter("@pMes", mes);



              return db.Database.SqlQuery<DisponibilidadEntity>("getDisponibilidadHotel @idHotel=@pHotel, @cantidadHabitacionesSolicitadas= @pCantHab , @cantidadPersonasSolicitadas=@pCantPers , @anio=@pAnio , @mes= @pMes ", paramComplejo, paramCantHab, paramCantPers, paramAnio, paramMes).ToList();


          }



      }







       
    }
}
