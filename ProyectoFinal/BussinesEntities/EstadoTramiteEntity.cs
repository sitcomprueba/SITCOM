using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BussinesEntities
{
    public class EstadoTramiteEntity
    {
        public EstadoTramiteEntity()
        {
            this.Tramite = new HashSet<Tramite>();
        }
    
        public int idEstadoTramite { get; set; }
        public string nombreEstadoTramite { get; set; }
    
        public virtual ICollection<Tramite> Tramite { get; set; }
    }
}
