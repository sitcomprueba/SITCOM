using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BussinesEntities;

namespace BL
{
    public class PerfilesManager
    {
        public List<Perfiles> GetAllPerfiles()
        {
            using(var db = new SitcomEntities())
            {
                var result = db.Perfiles.ToList();

                return result;
            }
        }
        public List<Paginas> GetAllPaginasPerfil(int idPerfil)
        {
            using(var db = new SitcomEntities())
            {
                var subquery = from pxp in db.PaginasXPerfil
                               where pxp.idPerfil == idPerfil
                               select pxp.idPagina;

                var result = (from p in db.Paginas
                              where subquery.Contains(p.idPagina)
                              select p).ToList();

                return result;
            }
        }

    }
}
