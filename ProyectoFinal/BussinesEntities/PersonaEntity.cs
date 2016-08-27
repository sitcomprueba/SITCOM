using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.ComponentModel.DataAnnotations;

namespace BussinesEntities
{
    using System;
    using System.Collections.Generic;

    public class PersonaEntity
    {
        public PersonaEntity()
        {
            this.Usuarios = new HashSet<UsuarioEntity>();
        }

        public int idPersona { get; set; }

        [Required(ErrorMessage="Se debe ingresar el nombre.")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "Se debe ingresar el apellido.")]
        public string apellido { get; set; }

        [Required(ErrorMessage = "Se debe ingresar el e-mail alternativo.")]
        public string email { get; set; }

        [Required(ErrorMessage = "Se debe ingresar el documento.")]
        public Nullable<int> documento { get; set; }

        [Required(ErrorMessage = "Se debe seleccionar el sexo.")]
        public Nullable<int> idSexo { get; set; }
        public Nullable<int> idTelefono { get; set; }

        [Required(ErrorMessage = "Se debe seleccionar el tipo de documento.")]
        public Nullable<int> idTipoDocumento { get; set; }
        public Nullable<int> idDomicilio { get; set; }

        public virtual DomicilioEntity Domicilio { get; set; }
        public virtual Sexo Sexo { get; set; }
        public virtual Telefono Telefono { get; set; }
        public virtual TipoDocumento TipoDocumento { get; set; }
        public virtual ICollection<UsuarioEntity> Usuarios { get; set; }
    }
}
