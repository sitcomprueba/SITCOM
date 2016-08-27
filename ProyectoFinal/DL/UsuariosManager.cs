using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinesEntities;
using DAL;

namespace BL
{
    public class UsuariosManager
    {
        public UsuarioEntity userActual;
        public ConvertidorEntitiesToDAL convert = new ConvertidorEntitiesToDAL();

        public List<UsuarioEntity> GetAllUsuarios()
        {
            using (var db = new SitcomEntities())
            {
                var result = (from u in db.Usuarios
                              select new UsuarioEntity()
                              {
                                  idUsuario = u.idUsuario,
                                  nombreUsuario = u.nombreUsuario,
                                  password = u.password,
                                  idPerfil = u.idPerfil,
                                  Perfiles = (from p in db.Perfiles
                                              where p.idPerfil == u.idPerfil
                                              select p).FirstOrDefault()
                              }).ToList<UsuarioEntity>();

                return result;
            }
        }
        public void DeleteUsuarios(int id)
        {
            using (var db = new SitcomEntities())
            {
                var result = (from u in db.Usuarios
                              where u.idUsuario == id
                              select u).FirstOrDefault();

                if (result != null)
                {
                    db.Usuarios.Remove(result);
                    db.SaveChanges();
                }
            }
        }
        public UsuarioEntity GetUsuarioById(int? id)
        {
            using (var db = new SitcomEntities())
            {
                var result = (from u in db.Usuarios
                              where u.idUsuario == id
                              select new UsuarioEntity()
                              {
                                  idUsuario = u.idUsuario,
                                  nombreUsuario = u.nombreUsuario,
                                  password = u.password,
                                  idPerfil = u.idPerfil,
                                  Perfiles = (from p in db.Perfiles
                                              where p.idPerfil == u.idPerfil
                                              select p).FirstOrDefault()
                              }).FirstOrDefault();

                return result;
            }
        }
        public UsuarioEntity GetUsuarioByNombre(string mail)
        {
            using (var db = new SitcomEntities())
            {
                var result = (from u in db.Usuarios
                              where u.nombreUsuario == mail
                              select new UsuarioEntity()
                              {
                                  idUsuario = u.idUsuario,
                                  nombreUsuario = u.nombreUsuario,
                                  password = u.password,
                                  idPerfil = u.idPerfil,
                                  Perfiles = (from p in db.Perfiles
                                              where p.idPerfil == u.idPerfil
                                              select p).FirstOrDefault()
                              }).FirstOrDefault();

                return result;
            }
        }
        public void AddUsuarios(UsuarioEntity u)
        {
            using(SitcomEntities db = new SitcomEntities())
            {
                var user = new Usuarios()
                {
                    nombreUsuario = u.nombreUsuario,
                    password = u.password,
                    idPerfil = u.idPerfil
                };

                db.Usuarios.Add(user);
                db.SaveChanges();
            }
        }
        public void UpdateUsuarios(UsuarioEntity us)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                var result = (from u in db.Usuarios
                             where u.idUsuario == us.idUsuario
                             select u).FirstOrDefault();

                if(result != null)
                {
                    result.nombreUsuario = us.nombreUsuario;
                    result.password = us.password;
                    result.idPerfil = us.idPerfil;
                    if(us.Persona != null)
                        result.Persona = convert.PersonaEntityToPersona(us.Persona);
                        
                    db.SaveChanges();
                }
                           
            }
        }
        public UsuarioEntity ValidarUsuario(string mail, string pass)
        {
            using(SitcomEntities db = new SitcomEntities())
            {
                var result = (from u in db.Usuarios
                              where u.nombreUsuario == mail && u.password == pass
                              select new UsuarioEntity()
                              {
                                  idUsuario = u.idUsuario,
                                  nombreUsuario = u.nombreUsuario,
                                  password = u.password,
                                  idPerfil = u.idPerfil,
                                  Perfiles = (from p in db.Perfiles
                                              where p.idPerfil == u.idPerfil
                                              select p).FirstOrDefault(),
                                  idPersona = u.idPersona
                              }).FirstOrDefault();

                return result; 
            }
       }
        public bool ValidarPermisoVista(UsuarioEntity us, string controlador, string vista)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                var result = (from u in db.PaginasXPerfil
                              where u.idPerfil == us.idPerfil
                              && u.idPagina == (from p in db.Paginas
                                                where p.controlador == controlador
                                                && p.vista  == vista
                                                select p.idPagina).FirstOrDefault()
                              select u).FirstOrDefault();
                              
                if(result != null)
                    return true;
                else
                    return false;   
            }
        }
 
    }
}
