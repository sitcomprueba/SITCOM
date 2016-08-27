using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesEntities
{
 public   class TipoHabitacionEntity
    {

        public TipoHabitacionEntity()
        {
            this.Habitacion = new HashSet<HabitacionEntity>();
        }
    
        public int idTipoHabitacion { get; set; }
        public string nombre { get; set; }
    
        public virtual ICollection<HabitacionEntity> Habitacion { get; set; }


    }
}
