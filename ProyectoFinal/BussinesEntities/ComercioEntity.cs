using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.ComponentModel.DataAnnotations;

namespace BussinesEntities
{
    public class ComercioEntity
    {
        public int idComercio { get; set; }
        public Nullable<int> idNegocio { get; set; }

        [Required(ErrorMessage = "¡Debés seleccionar un rubro!")]
        public Nullable<int> idRubro { get; set; }

        public virtual Negocio Negocio { get; set; }
        public virtual Rubro Rubro { get; set; }
    }
}
