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
 public   class HabitacionesManager
    {



        public List<DisponibilidadEntity> consultarDisponibilidad(int? idHab , int? anio, int? mes)
        {
            List<DisponibilidadEntity> lstDispo = new List<DisponibilidadEntity>();


            using (SitcomEntities db = new SitcomEntities())
            {

                SqlParameter paramHabitacion = new SqlParameter("@pHabitacion", idHab);
                SqlParameter paramAnio = new SqlParameter("@pAnio", anio);
                SqlParameter paramMes = new SqlParameter("@pMes", mes);



                return db.Database.SqlQuery<DisponibilidadEntity>("getDisponibilidadHabitacion @idHabitacion=@pHabitacion, @anio=@pAnio , @mes= @pMes ", paramHabitacion, paramAnio, paramMes).ToList();


            }



        }





    }
}
