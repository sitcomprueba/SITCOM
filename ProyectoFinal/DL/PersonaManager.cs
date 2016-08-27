using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinesEntities;
using DAL;

namespace BL
{
    public class PersonaManager
    {
        public bool ValidarPersona(UsuarioEntity us)
        {
            using(var db = new SitcomEntities())
            {
                var result = db.Persona.Where(per => per.idPersona == us.idPersona).FirstOrDefault();

                if (result != null)
                    return true;
                else 
                    return false;
            }
        }
    }
}
