using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BussinesEntities
{
    public class TramiteEntity
    {
        public int idTramite { get; set; }
        public Nullable<int> idEstadoTramite { get; set; }
        public Nullable<System.DateTime> fechaAlta { get; set; }
        public Nullable<System.DateTime> fechaFin { get; set; }
        public Nullable<int> idUsuarioSolicitante { get; set; }
        public Nullable<int> idUsuarioResponsable { get; set; }
        public Nullable<int> idNegocio { get; set; }
        public Nullable<int> idTipoTramite { get; set; }

        public string comentario { get; set; }

        public virtual EstadoTramite EstadoTramite { get; set; }
        public virtual TipoTramite TipoTramite { get; set; }
        public virtual Negocio Negocio { get; set; }
        public virtual Usuarios Usuarios { get; set; }
        public virtual Usuarios Usuarios1 { get; set; }
    }
}
