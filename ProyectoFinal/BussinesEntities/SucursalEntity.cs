using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BussinesEntities
{
    public class SucursalEntity
    {
        public SucursalEntity()
        {
            Domicilio = new DomicilioEntity();
        }
        public int idSucursal { get; set; }
        public string nombreSucursal { get; set; }
        public Nullable<int> idDomicilio { get; set; }
        public Nullable<int> idNegocio { get; set; }
        public string telefono { get; set; }
        public Nullable<bool> esPrincipal { get; set; }

        public virtual DomicilioEntity Domicilio { get; set; }
        public virtual NegocioEntity Negocio { get; set; }
    }
}
