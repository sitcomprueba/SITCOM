using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.ComponentModel.DataAnnotations;

namespace BussinesEntities
{
    public class UsuarioEntity
    {
        public UsuarioEntity()
        {
            this.FotosUsuario = new HashSet<FotosUsuario>();
            this.Negocio = new HashSet<Negocio>();
            this.Tramite = new HashSet<Tramite>();
        }
        public int idUsuario { get; set; }

        public Nullable<int> idPersona { get; set; }

        [Required(ErrorMessage = "¡Se debe completar el nombre del usuario!")]
        [EmailAddress(ErrorMessage = "¡El usuario debe ser un correo electrónico!")]
        public string nombreUsuario { get; set; }

        [Required(ErrorMessage = "¡Se debe completar el password!")]
        public string password { get; set; }

        [Required(ErrorMessage = "¡Debés confirmar el password!")]
        [Compare("password",ErrorMessage="¡Los password no coinciden!")]
        public string confirmarPassword { get; set; }
        public Nullable<int> idPerfil { get; set; }
       
        public virtual ICollection<FotosUsuario> FotosUsuario { get; set; }
        public virtual ICollection<Negocio> Negocio { get; set; }
        public virtual Perfiles Perfiles { get; set; }
        public virtual PersonaEntity Persona { get; set; }
        public virtual ICollection<Tramite> Tramite { get; set; }
       
    }
}
