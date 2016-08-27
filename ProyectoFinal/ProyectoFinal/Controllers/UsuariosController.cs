using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using BL;
using BussinesEntities;
using System.Web.Security;

namespace ProyectoFinal
{
    public class UsuariosController : Controller
    {
        private UsuarioEntity usuarioActual;
        private SitcomEntities db = new SitcomEntities();
        private UsuariosManager um = new UsuariosManager();
     
        public ActionResult Index()
        {
          if (ValidarPermisoVista("Usuarios", "Index"))
            {
                var usuarios = um.GetAllUsuarios();
                return View(usuarios);
            }
            else
            {
                return RedirectToAction("ErrorPermisos","Errores");
            }
        } //PANTALLA PRINCIPAL - USUARIOS
        public ActionResult Details(int? id)
        {
            if (ValidarPermisoVista("Usuarios", "Details"))
            {
                UsuarioEntity us = um.GetUsuarioById(id);
                return View(us);
            }
            else
            {
                ModelState.AddModelError("", "No podés acceder a esta página. Tu usuario NO tiene permisos");
                return RedirectToAction("Index", "Home");
            }
            
        } //PANTALLA DETALLES - USUARIOS
        public ActionResult Nuevo() //PANTALLA CREAR NUEVO - USUARIOS
        {
            ObtenerUsuarioActual();
            ViewBag.Perfil = usuarioActual.idPerfil;
            ViewBag.Perfiles = new SelectList(db.Perfiles, "idPerfil", "nombre");
            return View();
        }
        [HttpPost] //SE INDICA QUE EL METODO SE INVOCA A PARTIR DE UNA LLAMADA POST.
        [ValidateAntiForgeryToken]
        // SOBRECARGA QUE ENVÍA EL USUARIO A CREAR.
        public ActionResult Nuevo([Bind(Include="idUsuario,nombreUsuario,password,confirmarPassword,idPerfil")] UsuarioEntity usuario)
        {
            ObtenerUsuarioActual();

            if (usuario.idPerfil == null)
                usuario.idPerfil = 1;

            if (ModelState.IsValid)
            {
                if(um.GetUsuarioByNombre(usuario.nombreUsuario) == null)
                    um.AddUsuarios(usuario);
                else
                {
                    ModelState.AddModelError("", "El usuario ingresado ya está registrado. Por favor verifícalo.");
                    ViewBag.Perfil = usuarioActual.idPerfil;
                    ViewBag.Perfiles = new SelectList(db.Perfiles, "idPerfil", "nombre");
                    return View("Nuevo", usuario);
                }
            }

            return RedirectToAction("Index","Home");
        }
        public ActionResult Edit(int? id, string returnUrl)
        {
            if (ValidarPermisoVista("Usuarios", "Edit"))
            {
                ObtenerUsuarioActual();

                bool esMismoUsuario = false;

                if (usuarioActual.idUsuario == id)
                    esMismoUsuario = true;

                UsuarioEntity us = um.GetUsuarioById(id);
                ViewBag.MismoUsuario = esMismoUsuario;
                ViewBag.Perfiles = new SelectList(db.Perfiles, "idPerfil", "nombre", us.idPerfil);
                ViewBag.Perfil = usuarioActual.idPerfil;
                ViewBag.ReturnUrl = returnUrl;
                return View(us);                      
            }
            else
                return RedirectToAction("ErrorPermisos", "Errores");
         } //PANTALLA EDITAR - USUARIOS
        [HttpPost]  //SE INDICA QUE EL METODO SE INVOCA A PARTIR DE UNA LLAMADA POST.
        [ValidateAntiForgeryToken]
        // SOBRECARGA QUE ENVÍA EL USUARIO A EDITAR.
        public ActionResult Edit([Bind(Include="idUsuario,nombreUsuario,password,confirmarPassword,idPerfil")] UsuarioEntity usuarios, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                um.UpdateUsuarios(usuarios);
                return RedirectToAction(returnUrl);
            }
            ViewBag.idPerfil = new SelectList(db.Perfiles, "idPerfil", "nombre", usuarios.idPerfil);
            return View(usuarios);
        }
        public ActionResult EditUsuario()
        {
            ObtenerUsuarioActual();

            ViewBag.Perfil = usuarioActual.idPerfil;
            ViewBag.Perfiles = new SelectList(db.Perfiles, "idPerfil", "nombre",usuarioActual.idPerfil);

            return View(usuarioActual);
        }
        public ActionResult Delete(int? id)
        {
            if (ValidarPermisoVista("Usuarios", "Delete"))
            {
                UsuarioEntity us = um.GetUsuarioById(id);
                return View(us);
            }
            else
                return RedirectToAction("ErrorPermisos", "Errores");

        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // SOBRECARGA QUE ENVÍA EL USUARIO A EDITAR.
        public ActionResult DeleteConfirmed(int id)
        {
            um.DeleteUsuarios(id);
            return RedirectToAction("Index");
        }
        public ActionResult Login()
        {
            ObtenerUsuarioActual();

            if (usuarioActual.idUsuario == 0)
                return View();
            else
                return RedirectToAction("ErrorPermisos", "Errores");
        } //PANTALLA DE LOGIN DE USUARIOS
        public ActionResult LoginUser([Bind(Include="nombreUsuario,password")] UsuarioEntity us)
        {
            if(um.ValidarUsuario(us.nombreUsuario, us.password) != null)
            {
                us = um.ValidarUsuario(us.nombreUsuario, us.password);
                Session["User"] = us;
                FormsAuthentication.SetAuthCookie(us.nombreUsuario, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "El usuario o contraseña ingresados no son correctos.");
                return View("Login", us);
            }
        } //VALIDACIÓN Y LOGGEO DE USUARIO EN EL SISTEMA.
        public ActionResult LogOff()
        {
            FormsAuthentication.SetAuthCookie(null, false);
            Session["User"] = new UsuarioEntity();
            return RedirectToAction("Index", "Home");
        } //DESLOGGEO DEL USUARIO DEL SISTEMA.
        public ActionResult PanelControlUsuario(string mensaje)
        {
            if (mensaje == null)
                mensaje = "";

            ObtenerUsuarioActual();
            ViewBag.Perfiles = new SelectList(db.Perfiles, "idPerfil", "nombre", usuarioActual.idPerfil);
            ViewBag.Perfil = usuarioActual.idPerfil;
            ViewBag.Mensaje = mensaje;
            return View(usuarioActual);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public bool ValidarPermisoVista(string controlador,string vista) //METODO UNICO DEL CONTROLADOR PARA VALIDAR PERMISO DE LA VISTA (LLAMA AL MANEJADOR).
        {
           ObtenerUsuarioActual();
           return um.ValidarPermisoVista(usuarioActual, controlador, vista);
        }
        public void ObtenerUsuarioActual()
        {
            usuarioActual = (UsuarioEntity)Session["User"];
        }
    }
}
