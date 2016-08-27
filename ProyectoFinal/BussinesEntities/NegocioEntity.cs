using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace BussinesEntities
{
    using System;
    using System.Collections.Generic;

    public class NegocioEntity
    {
        public NegocioEntity()
        {
            this.Comercio = new HashSet<ComercioEntity>();
            this.FotosNegocio = new HashSet<FotosNegocio>();
            this.LugarHospedaje = new HashSet<LugarHospedajeEntity>();
            this.Sucursal = new HashSet<SucursalEntity>();
        }
    
        public int idNegocio { get; set; }

        [Required(ErrorMessage="¡Se debe completar el nombre del comercio!")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "¡Se debe completar el nombre del comercio!")]
        [StringLength(500, MinimumLength = 50, ErrorMessage="¡La descripción debe tener un mínimo de 50 caractéres!")]
        public string descripcion { get; set; }
        public Nullable<System.DateTime> fechaAlta { get; set; }
        public Nullable<int> idTipoNegocio { get; set; }
        public Nullable<int> idUsuario { get; set; }
        public Nullable<int> idNegocioModif { get; set; }
    
        public virtual ICollection<ComercioEntity> Comercio { get; set; }
        public virtual ICollection<FotosNegocio> FotosNegocio { get; set; }
        public virtual ICollection<LugarHospedajeEntity> LugarHospedaje { get; set; }
        public virtual TipoDeNegocio TipoDeNegocio { get; set; }
        public virtual Usuarios Usuarios { get; set; }
        public virtual ICollection<SucursalEntity> Sucursal { get; set; }
        public virtual ICollection<TramiteEntity> Tramite { get; set; }
    }
}
