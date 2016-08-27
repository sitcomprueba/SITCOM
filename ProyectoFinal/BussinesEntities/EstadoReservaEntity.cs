using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesEntities
{
  public  class EstadoReservaEntity
    {

      public EstadoReservaEntity()
        {
            this.Disponibilidad = new HashSet<DisponibilidadEntity>();
        }
    
        public int idEstado { get; set; }
        public string nombreEstado { get; set; }
    
        public virtual ICollection<DisponibilidadEntity> Disponibilidad { get; set; }


    }
}
