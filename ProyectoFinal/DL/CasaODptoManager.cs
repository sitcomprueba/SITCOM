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
  public  class CasaODptoManager
    {



        public List<DisponibilidadEntity> consultarDisponibilidad(int? idCasa, int? anio, int? mes)
        {
            List<DisponibilidadEntity> lstDispo = new List<DisponibilidadEntity>();


            using (SitcomEntities db = new SitcomEntities())
            {

                SqlParameter paramCasa = new SqlParameter("@pCasa", idCasa);
                SqlParameter paramAnio = new SqlParameter("@pAnio", anio);
                SqlParameter paramMes = new SqlParameter("@pMes", mes);



                return db.Database.SqlQuery<DisponibilidadEntity>("getDisponibilidadCasaDpto @idCasa=@pCasa, @anio=@pAnio , @mes= @pMes ", paramCasa, paramAnio, paramMes).ToList();


            }



        }





    }
}
