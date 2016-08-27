using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BussinesEntities;

namespace BL
{
    public class PersonasManager
    {
        ConvertidorEntitiesToDAL convert = new ConvertidorEntitiesToDAL();
        public int AddPersona(PersonaEntity per)
        {
            using(var db = new SitcomEntities())
            {
                var persona = convert.PersonaEntityToPersona(per);

                db.Persona.Add(persona);
                db.SaveChanges();

                var idPersona = db.Persona.OrderByDescending(p => p.idPersona).Select(p => p.idPersona).FirstOrDefault();

                int idUsuario = per.Usuarios.FirstOrDefault().idUsuario;
                var result = db.Usuarios.Where(us => us.idUsuario == idUsuario).FirstOrDefault();

                if(result != null)
                {
                    result.idPersona = int.Parse(idPersona.ToString());

                    db.SaveChanges();
                }

                return idPersona;

            }
        }
        public Persona GetPersonaById(int idPersona)
        {
            using(var db = new SitcomEntities())
            {
                var result = db.Persona.Include("Domicilio")
                                       .Include("Sexo")
                                       .Include("TipoDocumento")
                                       .Where(per => per.idPersona == idPersona).FirstOrDefault();

                return result;
            }
        }

    }
}
