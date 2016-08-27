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
    public class ComplejoManager
    {

        public List<CasaDptoOCabana> getCasaDptoById(int idComple)
        {

            using (var db = new SitcomEntities())
            {
                var result = (from u in db.CasaDptoOCabana
                              where u.idComplejo == idComple
                              select u).ToList<CasaDptoOCabana>();

                return result;

            }



        }


        public void registrarDisponibilidad(DisponibilidadEntity dispo)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                var dis = new Disponibilidad()
                {
                    fechaDesde = dispo.fechaDesde,
                    fechaHasta = dispo.fechaHasta,
                    idCasaODpto = dispo.idCasaODpto,
                    habilitado = dispo.habilitado,
                    idEstado = dispo.idEstado

                };

                db.Disponibilidad.Add(dis);
                db.SaveChanges();
            }
        }



        public List<DisponibilidadEntity> consultarDisponibilidad(int? idCompl, int? cantCas , int? cantPers ,int? anio , int? mes)
        {
            List<DisponibilidadEntity> lstDispo = new List<DisponibilidadEntity>();


            using (SitcomEntities db = new SitcomEntities())
            {

                SqlParameter paramComplejo = new SqlParameter("@pComple", idCompl);
                SqlParameter paramCantCas = new SqlParameter("@pCantCas", cantCas);
                SqlParameter paramCantPers = new SqlParameter("@pCantPers", cantPers);
                SqlParameter paramAnio = new SqlParameter("@pAnio", anio);
                SqlParameter paramMes = new SqlParameter("@pMes", mes);

           
             
                return db.Database.SqlQuery<DisponibilidadEntity>("getDisponibilidadComplejo @idComplejo=@pComple, @cantidadCasasSolicitadas= @pCantCas , @cantidadPersonasSolicitadas=@pCantPers , @anio=@pAnio , @mes= @pMes " , paramComplejo,paramCantCas,paramCantPers,paramAnio,paramMes).ToList();


            }



        }






    }

}


