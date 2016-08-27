using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinesEntities;

namespace BL
{
    public class DomicilioManager
    {
        private ConvertidorEntitiesToDAL convert = new ConvertidorEntitiesToDAL();
        public void AddDomicilio(DomicilioEntity d)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                var dom = convert.DomicilioEntityToDomicilio(d);
                db.Domicilio.Add(dom);
                db.SaveChanges();     
            }
        }
        public List<Pais> GetAllPaises()
        {
            using (var db = new SitcomEntities())
            {
                var result = (from p in db.Pais
                              select p).ToList<Pais>();
                
                return result;

            }

        }
        public List<Provincia> getProvinciaPaisSeleccionado(int? idPais)
        {
            using (var db = new SitcomEntities())
            {
                var result = (from prov in db.Provincia
                              where prov.idPais == idPais
                              select prov).ToList<Provincia>();

                return result;
            }

        }
        public List<Localidad> getLocalidadByProvinciaSeleccionada(int? idProvincia)
        {
            using (var db = new SitcomEntities())
            {
                var result = (from loc in db.Localidad
                              where loc.idProvincia == idProvincia
                              select loc).ToList<Localidad>();
                return result;
            }
        }        
        public List<Localidad> GetLocalidadesCercanas()
        {
            using(var db = new SitcomEntities())
            {
                var result = db.Localidad.Where(loc => loc.esCercana == true).ToList();

                return result;
            }
        }
    }
}
